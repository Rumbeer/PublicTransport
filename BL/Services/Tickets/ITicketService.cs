using BL.DTOs.Tickets;

namespace BL.Services.Tickets
{
    public interface ITicketService
    {
        /// <summary>
        /// creates new ticket from programs and custmer
        /// </summary>
        /// <param name="customerId">id of customer</param>
        /// <param name="programIds">ids of programs</param>
        TicketDTO CreateTicket(int customerId, int[] programIds);

        /// <summary>
        /// Makes reservatioin for the ticket
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
    }
}
