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


        public void CreateSpecificRoute(int routeId, DateTime departTime, int vehicleId)
        {
            routeService.CreateSpecificRoute(routeId, departTime, vehicleId);
        }

        public void CreateRoute(RouteDTO routeDTO)
        {
            routeService.CreateRoute(routeDTO);
        }

        public void DeleteStationFromRoute(int routeStationID)
        {
            routeService.DeleteStationFromRoute(routeStationID);
        }

        public List<RouteStationDTO> getRouteStationsByRoute(int routeId)
        {
            return routeService.getRouteStationsByRoute(routeId);
        }

        //List<Tuple<RouteDTO, List<RouteStationDTO>>> findRoutesWithStations(int departureStationID, int arriveStationID, DateTime departTime)


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

        public StationDTO getStationById(int stationID)
        {
            return stationService.GetStationById(stationID);
        }

        public List<StationDTO> getAllStationsByTown(string town)
        {
            return stationService.GetAllStationsByTown(town);
        }
    }
}
