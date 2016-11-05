using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    /// <summary>
    /// Base class for all eshop services
    /// </summary>
    public abstract class AppService
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
    }
}
