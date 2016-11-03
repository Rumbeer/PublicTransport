﻿using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace BL.Repositories
{
    public class StationRepository : EntityFrameworkRepository<Station, int>
    {
        public StationRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
