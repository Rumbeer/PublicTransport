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
    public class RouteStationForBetweenQuery : AppQuery<RouteStationDTO>
    {
        public RouteStationFilter Filter;
        public RouteStationForBetweenQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<RouteStationDTO> GetQueryable()
        {
            IQueryable<RouteStation> query = Context.RouteStations.Include(nameof(RouteStation.Route));
            if (Filter.RouteId != null)
            {
                query = query.Where(routeStation => routeStation.Route.ID == Filter.RouteId);
            }
            if (Filter.DepartStationId != null)
            {
                query = query.Where(routeStation => routeStation.Station.ID == Filter.DepartStationId);
            }
            if (Filter.ArrivalStationId != null)
            {
                query = query.Where(routeStation => routeStation.Station.ID == Filter.ArrivalStationId);
            }
            if (Filter.DepartFromFirstStation != null)
            {
                query = query.Where(routeStation => routeStation.DepartFromFirstStation == Filter.DepartFromFirstStation);
            }

            return query.ProjectTo<RouteStationDTO>();
        }
    }
}