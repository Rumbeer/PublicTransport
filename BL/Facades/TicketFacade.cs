using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Tickets;
using BL.DTOs.Programs;
using BL.DTOs.Tickets;
using BL.DTOs.Companies;
using BL.Services.Companies;

namespace BL.Facades
{
    public class TicketFacade
    {
        private readonly ITicketService ticketService;
        private readonly ICompanyService companyService;

        public TicketFacade(ITicketService ticketService, ICompanyService companyService)
        {
            this.ticketService = ticketService;
            this.companyService = companyService;
        }

        public void CreateTicket(int customerId, int companyId, TicketDTO ticketDto, List<ProgramDTO> programDtos)
        {
            ticketService.CreateTicket(customerId, companyId, ticketDto, programDtos);
        }
        
        public void TicketReservation(int ticketId)
        {
            ticketService.TicketReservation(ticketId);
        }

        public void TicketRefund(int ticketId)
        {
            ticketService.TicketRefund(ticketId);
        }

        public void ClaimDiscount(int ticketId, int discountId, bool changingDiscount, string code)
        {
            ticketService.ClaimDiscount(ticketId, discountId, changingDiscount, code);
        }

        void CreateCompany(CompanyDTO companyDto)
        {
            companyService.CreateCompany(companyDto);
        }

        void EditCompany(CompanyDTO companyDto)
        {
            companyService.EditCompany(companyDto);
        }

        void DeleteCompany(int companyId)
        {
            companyService.DeleteCompany(companyId);
        }

        CompanyDTO GetCompanyById(int companyId)
        {
            return companyService.GetCompanyById(companyId);
        }

        int GetCompanyIdByName(string companyName)
        {
            return companyService.GetCompanyIdByName(companyName);
        }

        IEnumerable<CompanyDTO> ListAllCompanies()
        {
            return companyService.ListAllCompanies();
        }
    }
}
