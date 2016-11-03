using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class SeatRepository : EntityFrameworkRepository<Seat, int>
    {
        public SeatRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
