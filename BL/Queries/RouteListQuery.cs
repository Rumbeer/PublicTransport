using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.DTOs.Routes;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class RouteListQuery : AppQuery<RouteDTO>
    {
        public RouteFilter Filter { get; set; }

        public RouteListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<RouteDTO> GetQueryable()
        {
            IQueryable<Route> query = Context.Routes;
            query.Where(route => route.RouteStations.Any(routeStation => routeStation.ID == Filter.RouteStationID));
            return query.ProjectTo<RouteDTO>();
        }
    }
}

