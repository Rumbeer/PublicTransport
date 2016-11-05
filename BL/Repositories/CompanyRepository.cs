using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class CompanyRepository : EntityFrameworkRepository<Company, int>
    {
        public CompanyRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
