using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.DTOs.RouteStations;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class StationInRouteStationQuery : AppQuery<RouteStationDTO>
    {
        public StationFilter Filter { get; set; }
        public StationInRouteStationQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<RouteStationDTO> GetQueryable()
        {
            IQueryable<RouteStation> query = Context.RouteStations;
            query = query.Where(routeStation => routeStation.Station.ID == Filter.Id);
            return query.ProjectTo<RouteStationDTO>();
        }
    }
}
