using BL.DTOs.Tickets;
using BL.DTOs.Programs;
using System.Collections.Generic;

namespace BL.Services.Tickets
{
    public interface ITicketService
    {
        /// <summary>
        /// Creates new ticket
        /// </summary>
        /// <param name="customerId">id of customer</param>
        /// <param name="companyId">id of company</param>
        /// <param name="ticketDto">ticket details</param>
        /// <param name="programDtos">list of programs</param>
        void CreateTicket(int customerId, int companyId, TicketDTO ticketDto, List<ProgramDTO> programDtos);

        /// <summary>
        /// Makes reservation for the ticket
        /// </summary>
        /// <param name="ticketId">id of ticket</param>
        void TicketReservation(int ticketId);

        /// <summary>
        /// Refunds ticket if company supports that
        /// </summary>
        /// <param name="ticketId">id of ticket</param>
        void TicketRefund(int ticketId);

        /// <summary>
        /// Aplies discount for a ticket
        /// </summary>
        /// <param name="ticketId">id of ticket</param>
        /// <param name="discountId">id of discount</param>
        /// <param name="changingDiscount">true if the customer wants to claim different discount than he already claimed</param>
        void ClaimDiscount(int ticketId, int discountId, bool changingDiscount, string code);

        /// <summary>
        /// lists all tickets
        /// </summary>
        /// <returns></returns>
        List<TicketDTO> ListAllTickets();

        /// <summary>
        /// Gets ticket by id
        /// </summary>
        /// <param name="ticketId">id of ticket</param>
        /// <returns></returns>
        TicketDTO GetTicketById(int ticketId);
    }
}
