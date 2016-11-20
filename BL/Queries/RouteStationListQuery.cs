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
    public class RouteStationListQuery : AppQuery<RouteStationDTO>
    {
        public RouteStationFilter Filter;
        public RouteStationListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<RouteStationDTO> GetQueryable()
        {
            IQueryable<RouteStation> query = Context.RouteStations.Include(nameof(RouteStation.Route));
            if (Filter.RouteId != null)
            {
                query = query.Where(routeStation => routeStation.Route.ID == Filter.RouteId);
            }
            if(!(Filter.ArrivalStationId == null) || !(Filter.DepartStationId == null))
            {
                RouteStation departRouteStation = query.Where(routeStation => routeStation.Station.ID == Filter.DepartStationId).FirstOrDefault();
                RouteStation arrivalRouteStation = query.Where(routeStation => routeStation.Station.ID == Filter.ArrivalStationId).FirstOrDefault();
                if (departRouteStation == null || arrivalRouteStation == null)
                {
                    throw new ArgumentNullException("RouteStation cannot be null");
                }

                if (Filter.DepartStationId != null && Filter.ArrivalStationId != null)
                {
                    query = query.Where(routeStation => routeStation.Order >= departRouteStation.Order && routeStation.Order <= arrivalRouteStation.Order);
                }
            }
            return query.ProjectTo<RouteStationDTO>();
        }
    }
}
