using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using BL.DTOs;
using BL.Queries;
using AutoMapper;
using BL.Repositories;
using BL.DTOs.Filters;
using BL.DTOs.Seats;
using BL.DTOs.Routes;
using BL.DTOs.RouteStations;
using BL.DTOs.Programs;

namespace BL.Services.Routes
{
    public class RouteService : AppService, IRouteService
    {
        private readonly RouteRepository routeRepository;
        private readonly RoutesStationRepository routeStationRepository;
        private readonly ProgramRepository programRepository;
        private readonly FindProgramsOfRouteStationQuery findProgramsOfRouteStationQuery;
        private readonly RouteStationListQuery routeStationListQuery;
        private readonly RouteListQuery routeListQuery;
        private readonly CreateSpecificRouteQuery createSpecificRouteQuery;
        private readonly StationRepository stationRepository;
        private readonly SeatRepository seatRepository;
        private readonly SeatListQuery seatListQuery;
        private readonly EmptyProgramsListQuery emptyProgramsListQuery;
        private readonly RouteListAllQuery routeListAllQuery;

        public RouteService(RouteListAllQuery routeListAllQuery, EmptyProgramsListQuery emptyProgramsListQuery, SeatRepository seatRepository, SeatListQuery seatListQuery, RouteRepository routeRepository, RoutesStationRepository routeStationRepository,
            ProgramRepository programRepository, FindProgramsOfRouteStationQuery findProgramsOfRouteStationQuery,
            RouteStationListQuery routeStationListQuery, RouteListQuery routeListQuery, CreateSpecificRouteQuery createSpecificRouteQuery,
            StationRepository stationRepository)
        {
            this.routeListAllQuery = routeListAllQuery;
            this.emptyProgramsListQuery = emptyProgramsListQuery;
            this.seatRepository = seatRepository;
            this.seatListQuery = seatListQuery;
            this.routeRepository = routeRepository;
            this.routeStationRepository = routeStationRepository;
            this.programRepository = programRepository;
            this.findProgramsOfRouteStationQuery = findProgramsOfRouteStationQuery;
            this.routeStationListQuery = routeStationListQuery;
            this.routeListQuery = routeListQuery;
            this.createSpecificRouteQuery = createSpecificRouteQuery;
            this.stationRepository = stationRepository;
        }

        public void CreateSpecificRoute(int routeId, DateTime departTime, int vehicleId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var route = routeRepository.GetById(routeId, r => r.RouteStations, s => s.RouteStations);
                createSpecificRouteQuery.RouteId = routeId;
                var routeStationsTemplates = createSpecificRouteQuery.Execute().ToList();
                var seatDTOs = ListSeatsOfVehicle(vehicleId);

                foreach (var routeStationTemplate in routeStationsTemplates)
                {
                    var routeStation = new RouteStation();
                    Mapper.Map(routeStationTemplate, routeStation);
                    routeStation.DepartFromFirstStation = departTime;
                    var routeStationForGettingStation = routeStationRepository.GetById(routeStationTemplate.ID, routeStationTemplat => routeStationTemplat.Station);

                    var station = stationRepository.GetById(routeStationForGettingStation.Station.ID, s => s.RouteStations);

                    routeStation.Route = route;
                    route.RouteStations.Add(routeStation);

                    routeStation.Station = station;
                    station.RouteStations.Add(routeStation);

                    if (seatDTOs != null)
                    {
                        foreach (var seatDTO in seatDTOs)
                        {
                            var seat = seatRepository.GetById(seatDTO.ID, s => s.Programs);
                            var program = new Program
                            {
                                IsSeatOccupied = false,
                                RouteStation = routeStation,
                                Seat = seat
                            };
                            seat.Programs.Add(program);
                            programRepository.Insert(program);
                            routeStation.Programs.Add(program);
                        }
                    }
                    routeStationRepository.Insert(routeStation);
                }
                uow.Commit();
            }
        }

        //to do edit - create specific route, delete programs
        public void CreateRoute(RouteDTO routeDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var route = Mapper.Map<Route>(routeDTO);
                routeRepository.Insert(route);
                uow.Commit();
            }
        }

        public void DeleteStationFromRoute(int routeStationID)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                routeStationRepository.Delete(routeStationID);
                uow.Commit();
            }
        }

        public List<RouteStationDTO> getRouteStationsByRoute(int routeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                routeStationListQuery.Filter = new RouteStationFilter { RouteId = routeId };
                return routeStationListQuery.Execute().ToList();
            }
        }

        public List<Tuple<RouteDTO, List<RouteStationDTO>>> findRoutesWithStations(int departureStationID, int arriveStationID, DateTime departTime)
        {
            using (UnitOfWorkProvider.Create())
            {
                routeListQuery.Filter = new RouteFilter
                {
                    DepartStationID = departureStationID,
                    ArrivalStationID = arriveStationID
                };
                List<RouteDTO> routes = routeListQuery.Execute().ToList();
                var resultRoutesList = new List<Tuple<RouteDTO, List<RouteStationDTO>>>();
                foreach (var routeItem in routes)
                {
                    routeStationListQuery.Filter = new RouteStationFilter
                    {
                        RouteId = routeItem.ID,
                        DepartStationId = departureStationID,
                        ArrivalStationId = arriveStationID
                    };
                    List<RouteStationDTO> routesStationDTOs = routeStationListQuery.Execute().ToList();
                    resultRoutesList.Add(Tuple.Create(routeItem, routesStationDTOs));
                }
                return resultRoutesList;
            }
        }

        public void AddRouteStation(int stationId, int routeId, RouteStationDTO routeStationDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var routeStation = Mapper.Map<RouteStation>(routeStationDTO);
                var route = routeRepository.GetById(routeId, s => s.RouteStations);
                var station = stationRepository.GetById(stationId, s => s.RouteStations);

                routeStation.Route = route;
                route.RouteStations.Add(routeStation);
                routeStation.Order = route.RouteStations.Count();

                routeStation.Station = station;
                station.RouteStations.Add(routeStation);

                routeStationRepository.Insert(routeStation);
                uow.Commit();
            }
        }

        public List<SeatDTO> ListSeatsOfVehicle(int vehicleId)
        {
            using (UnitOfWorkProvider.Create())
            {
                seatListQuery.Filter = new SeatFilter() { VehicleId = vehicleId };
                return seatListQuery.Execute().ToList();
            }
        }

        public List<List<ProgramDTO>> ListEmptyProgramsOfRouteStations(int[] routeStationIds)
        {
            using (UnitOfWorkProvider.Create())
            {
                var routeStation = routeStationRepository.GetById(routeStationIds[0]);
                var seatCount = RouteStationSeatCount(routeStation.ID);
                var listOfPrograms = new List<List<ProgramDTO>>();
                for (int i = 1; i <= seatCount; i++)
                {
                    bool isEmpty = true;
                    var listOfEmptyPrograms = new List<ProgramDTO>();
                    for (int j = 0; j < routeStationIds.Count(); j++)
                    {
                        emptyProgramsListQuery.RouteStationId = routeStationIds[j];
                        emptyProgramsListQuery.SeatNumber = i;
                        var program = emptyProgramsListQuery.Execute().ToList();
                        if (program != null && program.FirstOrDefault() != null && !program.FirstOrDefault().IsSeatOccupied)
                        {
                            listOfEmptyPrograms.Add(program.FirstOrDefault());
                        }
                        else
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                    if (isEmpty)
                    {
                        listOfPrograms.Add(listOfEmptyPrograms);
                    }
                }
                return listOfPrograms;
            }
            
        }
        public List<RouteDTO> ListAllRoutes()
        {
            using (UnitOfWorkProvider.Create())
            {
                return routeListAllQuery.Execute().ToList();
            }
        }

        public void DeleteRoute(int routeId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                routeRepository.Delete(routeId);
                uow.Commit();
            }
        }

        private int RouteStationSeatCount(int routeStationId)
        {
            using (UnitOfWorkProvider.Create())
            {
                //routeStation should have exactly one program for each seat
                var routeStation = routeStationRepository.GetById(routeStationId, r => r.Programs);
                return routeStation.Programs.Count();
            }
        }
    }
}
