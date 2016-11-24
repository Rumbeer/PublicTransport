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

        void CreateRoute(RouteDTO routeDTO, int companyId);

        void DeleteStationFromRoute(int routeStationID);

        List<RouteStationDTO> GetRouteStationsByRoute(int routeId);

        List<Tuple<RouteStationDTO, RouteStationDTO>> FindRoutesWithStations(int departureStationID, int arriveStationID, DateTime departTime);

        int GetRouteIdByRouteStation(int routeStationId);

        void AddRouteStation(int stationId, int routeId, RouteStationDTO routeStationDTO);

        List<SeatDTO> ListSeatsOfVehicle(int vehicleId);

        List<List<ProgramDTO>> ListEmptyProgramsOfRouteStations(int[] routeStationIds);

        List<RouteDTO> ListAllRoutes();

        RouteListQueryResultDTO ListCompanyRoutes(int companyId, int page = 1);

        void DeleteRoute(int routeId);
    }
}
