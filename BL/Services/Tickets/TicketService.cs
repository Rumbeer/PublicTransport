using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Tickets;
using BL.DTOs.Filters;
using BL.DTOs.Programs;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using System;


namespace BL.Services.Tickets
{
    public class TicketService : AppService, ITicketService
    {

        #region Dependencies

        private readonly TicketRepository ticketRepository;

        private readonly ProgramRepository programRepository;

        private readonly DiscountRepository discountRepository;

        private readonly CustomerRepository customerRepository;

        private readonly CompanyRepository companyRepository;

        private readonly TicketListAllQuery ticketListAllQuery;

        public TicketService(TicketListAllQuery ticketListAllQuery, TicketRepository ticketRepository, ProgramRepository programRepository, DiscountRepository discountRepository, CustomerRepository customerRepository, CompanyRepository companyRepository)
        {
            this.ticketListAllQuery = ticketListAllQuery;
            this.companyRepository = companyRepository;
            this.customerRepository = customerRepository;
            this.ticketRepository = ticketRepository;
            this.programRepository = programRepository;
            this.discountRepository = discountRepository;
        }
        #endregion

        public void CreateTicket(int customerId, int companyId, TicketDTO ticketDto, List<ProgramDTO> programDtos)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = new Ticket();
                Mapper.Map(ticketDto, ticket);
                var customer = customerRepository.GetById(customerId, c => c.Tickets);
                var company = companyRepository.GetById(companyId, c => c.Tickets);
                if(company == null || customer == null)
                {
                    throw new ArgumentNullException("Ticket service - CreateTicket(...) company and customer cant be null");
                }

                customer.Tickets.Add(ticket);
                company.Tickets.Add(ticket);

                ticket.Customer = customer;
                ticket.Company = company;

                ticket.Programs = new List<Program>();
                foreach(var programDto in programDtos){
                    var program = programRepository.GetById(programDto.ID, p => p.Ticket, p => p.RouteStation);
                    program.Ticket = ticket;
                    ticket.Programs.Add(program);
                    ticket.TotalDistance += program.RouteStation.DistanceFromPreviousStation;
                }

                ticket.IsConfirmed = false;
                ticket.IsRefunded = false;

                ticketRepository.Insert(ticket);
                uow.Commit();
            }
        }

        public void TicketReservation(int ticketId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Programs, t => t.Company, t => t.Customer, t => t.Discount);
                if (ticket == null)
                {
                    throw new ArgumentNullException("Ticket service - TicketReservation(...) ticket cant be null");
                }
                if (ticket.Programs == null || ticket.Programs.Count() == 0)
                {
                    throw new ArgumentNullException("Ticket service - TicketReservation(...) programs cant be null");
                }
                foreach (var program in ticket.Programs)
                {
                    if (program.IsSeatOccupied)
                    {
                        throw new ArgumentException("Ticket service - TicketReservation(...) seat is already occupied");
                    }
                    var p = programRepository.GetById(program.ID, pr => pr.RouteStation, pr => pr.Ticket, pr => pr.Seat);
                    p.IsSeatOccupied = true;
                    programRepository.Update(p);
                }
                ticket.IsConfirmed = true;
                ticket.Price = CalculatePrice(ticketId);
                ticketRepository.Update(ticket);
                uow.Commit();
            }
        }

        public void TicketRefund(int ticketId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Programs, t => t.Company, t => t.Customer, t => t.Discount);
                if(ticket == null)
                {
                    throw new ArgumentNullException("Ticket service - TicketRefund(...) ticket cant be null");
                }
                if (!ticket.IsConfirmed)
                {
                    throw new ArgumentException("Ticket service - TicketRefund(...) tickes has not been corfimed yet");
                }
                if (!ticket.Company.RedeemableTicket)
                {
                    throw new ArgumentException("Ticket service - TicketRefund(...) company does not support ticket refunding");
                }
                //(ticket.Company.TimeToRedeem should never be null: checking above
                if (DateTime.Now.Add(ticket.Company.TimeToRedeem.GetValueOrDefault()) > ticket.Departure)
                {
                    throw new ArgumentException("Ticket service - TicketRefund(...) ticket cant be refunded now");
                }
                foreach (var program in ticket.Programs)
                {
                    var p = programRepository.GetById(program.ID, pr => pr.RouteStation, pr => pr.Ticket, pr => pr.Seat);
                    p.IsSeatOccupied = false;
                    programRepository.Update(p);
                }
                ticket.IsRefunded = true;
                ticketRepository.Update(ticket);
                uow.Commit();
            }
        }

        public void ClaimDiscount(int ticketId, int discountId, bool changingDiscount, string code)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Programs, t => t.Company, t => t.Customer, t => t.Discount);
                if (ticket == null)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) ticket cant be null");
                }
                if (!changingDiscount && ticket.Discount != null)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) ticket cant have two discounts");
                }
                var discount = discountRepository.GetById(discountId, d => d.Company, d => d.Tickets);
                if (discount == null || discount.Company.ID != ticket.Company.ID)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) company does not support this discount");
                }
                if (discount.DiscountType == DAL.Enum.DiscountType.Special && !discount.Code.Equals(code))
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) Incorrect code");
                }
                ticket.Discount = discount;
                discount.Tickets.Add(ticket);
                ticket.Price = CalculatePrice(ticketId);
                ticketRepository.Update(ticket);
                uow.Commit();
            }
        }

        public double CalculatePrice(int ticketId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Company, t => t.Discount);
                if (ticket == null)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) ticket cant be null");
                }
                return ticket.Discount == null ? ticket.Company.CostPerKm * ticket.TotalDistance : ticket.Company.CostPerKm * ticket.TotalDistance * ((double)ticket.Discount.Value/100);
            }
        }

        public List<TicketDTO> ListAllTickets()
        {
            using (UnitOfWorkProvider.Create())
            {
                return ticketListAllQuery.Execute().ToList();
            }
        }

        public TicketDTO GetTicketById(int ticketId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return Mapper.Map<TicketDTO>(ticketRepository.GetById(ticketId));
            }
        }

    }
}
