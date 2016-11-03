using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class VehicleRepository : EntityFrameworkRepository<Vehicle, int>
    {
        public VehicleRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
