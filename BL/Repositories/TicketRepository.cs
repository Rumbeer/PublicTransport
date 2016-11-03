using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class TicketRepository : EntityFrameworkRepository<Ticket, int>
    {
        public TicketRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
