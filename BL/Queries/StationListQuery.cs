using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Filters;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using BL.AppInfrastructure;
using BL.DTOs;
using AutoMapper.QueryableExtensions;
using BL.DTOs.Stations;

namespace BL.Queries
{
    public class StationListQuery : AppQuery<StationDTO>
    {
        public StationFilter Filter;

        public StationListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<StationDTO> GetQueryable()
        {
            IQueryable<Station> query = Context.Stations;

            if (!string.IsNullOrEmpty(Filter.Town))
            {
                query = query.Where(station => station.Town == Filter.Town);
            }

            if (!string.IsNullOrEmpty(Filter.Name))
            {
                query = query.Where(station => station.Name == Filter.Name);
            }

            return query.ProjectTo<StationDTO>();
        }
    }
}
