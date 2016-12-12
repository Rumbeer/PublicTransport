using BL.DTOs.Programs;
using BL.DTOs.Routes;
using BL.DTOs.RouteStations;
using BL.DTOs.Seats;
using BL.DTOs.Stations;
using System;
using System.Collections.Generic;

namespace BL.Services.Routes
{
    public interface IRouteService
    {
        int PageSize { get; }

        List<DateTime> GetRouteDepartTimes(int routeId);

        List<RouteStationDTO> GetRouteTemplate(int routeId);

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

        StationDTO GetStationByRouteStation(int routeStationId);

        /// <summary>
        /// returns Ids of RouteStations that are between the RouteStations with id from and RouteStations with id to
        /// </summary>
        /// <param name="from">id of first RouteStations</param>
        /// <param name="to">id of last RouteStations</param>
        /// <returns></returns>
        int[] GetRouteStationsBetween(int from, int to);
    }
}
