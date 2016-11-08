using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Tickets;
using BL.DTOs.Filters;
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

        public TicketService(TicketRepository ticketRepository, ProgramRepository programRepository, DiscountRepository discountRepository)
        {
            this.ticketRepository = ticketRepository;
            this.programRepository = programRepository;
            this.discountRepository = discountRepository;
        }
        #endregion

        public TicketDTO CreateTicket(int customerId, int[] programIds)
        {

            throw new NotImplementedException();
        }

        public void TicketReservation(int ticketId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Programs);
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
                    program.IsSeatOccupied = true;
                }
                ticket.IsConfirmed = true;
                uow.Commit();
            }
        }

        public void TicketRefund(int ticketId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Company, t => t.Programs);
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
                    program.IsSeatOccupied = false;
                }
                ticket.IsRefunded = true;
                uow.Commit();
            }
        }

        public void ClaimDiscount(int ticketId, int discountId, bool changingDiscount, string code)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ticket = ticketRepository.GetById(ticketId, t => t.Company, t => t.Discount);
                if (ticket == null)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) ticket cant be null");
                }
                if (!changingDiscount && ticket.Discount != null)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) ticket cant have two discounts");
                }
                var discount = discountRepository.GetById(discountId, d => d.Company);
                if (discount == null || discount.Company.ID != ticket.Company.ID)
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) company does not support this discount");
                }
                if (discount.DiscountType == DAL.Enum.DiscountType.Special && !discount.Code.Equals(code))
                {
                    throw new ArgumentNullException("Ticket service - ClaimDiscount(...) Incorrect code");
                }
                ticket.Discount = discount;
                ticket.Price = CalculatePrice(ticketId);
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
    }
}
