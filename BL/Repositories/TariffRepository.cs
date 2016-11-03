using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class TariffRepository : EntityFrameworkRepository<Tariff, int>
    {
        public TariffRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
