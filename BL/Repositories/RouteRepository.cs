using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class RouteRepository : EntityFrameworkRepository<Route, int>
    {
        public RouteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
