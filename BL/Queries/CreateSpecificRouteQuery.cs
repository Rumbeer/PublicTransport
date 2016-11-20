using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;
using BL.DTOs.RouteStations;

namespace BL.Queries
{
    public class CreateSpecificRouteQuery : AppQuery<RouteStationDTO>
    {
        public CreateSpecificRouteQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public int RouteId { get; set; }

        protected override IQueryable<RouteStationDTO> GetQueryable()
        {
            IQueryable<RouteStation> query = Context.RouteStations.Include(nameof(RouteStation.Route));
            if (RouteId > 0)
            {
                query = query.Where(routeStation => routeStation.Route.ID == RouteId);
                query = query.Where(routeStation => routeStation.DepartFromFirstStation == null);
                return query.ProjectTo<RouteStationDTO>();
            }
            return null;
        }
    }
}
