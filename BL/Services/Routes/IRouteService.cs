using BL.DTOs.Programs;
using BL.DTOs.Routes;
using BL.DTOs.RouteStations;
using BL.DTOs.Seats;
using System;
using System.Collections.Generic;

namespace BL.Services.Routes
{
    public interface IRouteService
    {
        void CreateSpecificRoute(int routeId, DateTime departTime, int vehicleId);

        void CreateRoute(RouteDTO routeDTO);

        void DeleteStationFromRoute(int routeStationID);

        List<RouteStationDTO> getRouteStationsByRoute(int routeId);

        List<Tuple<RouteDTO, List<RouteStationDTO>>> findRoutesWithStations(int departureStationID, int arriveStationID, DateTime departTime);

        void AddRouteStation(int stationId, int routeId, RouteStationDTO routeStationDTO);

        List<SeatDTO> ListSeatsOfVehicle(int vehicleId);

        List<List<ProgramDTO>> ListEmptyProgramsOfRouteStations(int[] routeStationIds);

        List<RouteDTO> ListAllRoutes();

        void DeleteRoute(int routeId);
    }
}
