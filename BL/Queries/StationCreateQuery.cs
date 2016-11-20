using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.DTOs.Stations;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class StationCreateQuery : AppQuery<StationDTO>
    {
        public StationFilter Filter;

        public StationCreateQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<StationDTO> GetQueryable()
        {
            IQueryable<Station> query = Context.Stations;
            query = query.Where(station => station.Name == Filter.Name && station.Town == Filter.Town);
            return query.ProjectTo<StationDTO>();
        }
    }
}
