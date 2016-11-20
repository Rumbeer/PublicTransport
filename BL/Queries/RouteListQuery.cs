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
            IQueryable<RouteStation> queryRouteStation = Context.RouteStations;
            queryRouteStation = queryRouteStation.Where(routeStation => routeStation.Station.ID == Filter.DepartStationID || routeStation.Station.ID == Filter.ArrivalStationID);
            if(queryRouteStation.Count() != 2)
            {
                throw new ArgumentException();
            }
            query = query.Where(route => route.RouteStations.Contains(queryRouteStation.First()) && route.RouteStations.Contains(queryRouteStation.Last()));
            return query.ProjectTo<RouteDTO>();

        }
    }
}
