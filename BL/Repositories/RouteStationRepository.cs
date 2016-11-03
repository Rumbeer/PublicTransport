using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class RoutesStationRepository : EntityFrameworkRepository<RouteStation, int>
    {
        public RoutesStationRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
