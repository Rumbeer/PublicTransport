using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class ProgramRepository : EntityFrameworkRepository<Program, int>
    {
        public ProgramRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
