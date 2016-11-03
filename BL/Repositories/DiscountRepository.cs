using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class DiscountRepository : EntityFrameworkRepository<Discount, int>
    {
        public DiscountRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
