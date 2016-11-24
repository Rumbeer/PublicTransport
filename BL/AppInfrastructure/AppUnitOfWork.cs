using System;
using System.Data.Entity;
using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.AppInfrastructure
{
    public class AppUnitOfWork : EntityFrameworkUnitOfWork
    {
        public new AppDbContext2 Context => (AppDbContext2)base.Context;

        public AppUnitOfWork(IUnitOfWorkProvider provider, Func<DbContext> dbContextFactory, DbContextOptions options)
            : base(provider, dbContextFactory, options) { }
    }
}
