using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Routes;
using BL.DTOs.Seats;
using BL.DTOs.RouteStations;
using BL.DTOs.Routes;
using BL.Services.Stations;
using BL.DTOs.Stations;
using BL.DTOs.Filters;

namespace BL.Facades
{
    public class RouteFacade
    {
        private readonly IRouteService routeService;
        private readonly IStationService stationService;
        public RouteFacade(IRouteService routeService, IStationService stationService)
        {
            this.routeService = routeService;
            this.stationService = stationService;
        }

        public int RoutePageSize => routeService.PageSize;

        public void CreateSpecificRoute(int routeId, DateTime departTime, int vehicleId)
        {
            routeService.CreateSpecificRoute(routeId, departTime, vehicleId);
        }

        public void CreateRoute(RouteDTO routeDTO, int companyId)
        {
            routeService.CreateRoute(routeDTO, companyId);
        }

        public void DeleteRoute(int routeId)
        {
            routeService.DeleteRoute(routeId);
        }

        public void DeleteStationFromRoute(int routeStationID)
        {
            routeService.DeleteStationFromRoute(routeStationID);
        }

        public RouteListQueryResultDTO ListCompanyRoutes(int companyId, int page = 1)
        {
            return routeService.ListCompanyRoutes(companyId, page);
        }

        public List<RouteStationDTO> GetRouteStationsByRoute(int routeId)
        {
            return routeService.GetRouteStationsByRoute(routeId);
        }

        public List<RouteDTO> ListAllRoutes()
        {
            return routeService.ListAllRoutes();
        }

        public void AddRouteStation(int stationId, int routeId, RouteStationDTO routeStationDTO)
        {
            routeService.AddRouteStation(stationId, routeId, routeStationDTO);
        }

        public List<SeatDTO> ListSeatsOfVehicle(int vehicleId)
        {
            return routeService.ListSeatsOfVehicle(vehicleId);
        }

        public void CreateStation(StationDTO stationDTO)
        {
            stationService.CreateStation(stationDTO);
        }

        public void DeleteStation(int stationID)
        {
            stationService.DeleteStation(stationID);
        }

        public StationDTO GetStationById(int stationID)
        {
            return stationService.GetStationById(stationID);
        }

        public List<StationDTO> GetAllStationsByTown(string town)
        {
            return stationService.GetAllStationsByTown(town);
        }


        public string GetStationNameByRouteStation(int routeStationId)
        {
            return stationService.GetStationNameByRouteStation(routeStationId);
        }

        public List<StationDTO> GetAllStationsByFilter(StationFilter filter)
        {
            return stationService.GetStationsByFilter(filter);
        }

        public List<RouteStationDTO> GetRouteTemplate(int routeId)
        {
            return routeService.GetRouteTemplate(routeId);
        }

        public List<DateTime> GetRouteDepartTimes(int routeId)
        {
            return routeService.GetRouteDepartTimes(routeId);
        }
    }
}
