using BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs;
using AutoMapper;
using DAL.Entities;
using BL.Queries;
using BL.DTOs.Filters;
using BL.DTOs.Stations;

namespace BL.Services.Stations
{
    public class StationService : AppService, IStationService
    {
        private readonly StationRepository stationRepository;
        private readonly RoutesStationRepository routeStationRepository;
        private readonly StationListQuery stationListQuery;
        private readonly StationCreateQuery stationCreateQuery;
        private readonly StationInRouteStationQuery stationInRouteStationQuery;

        public StationService(StationRepository stationRepository, RoutesStationRepository routeStationRepository, 
            StationListQuery stationListQuery, StationCreateQuery stationCreateQuery, StationInRouteStationQuery stationInRouteStationQuery)
        {
            this.stationRepository = stationRepository;
            this.routeStationRepository = routeStationRepository;
            this.stationListQuery = stationListQuery;
            this.stationCreateQuery = stationCreateQuery;
            this.stationInRouteStationQuery = stationInRouteStationQuery;
        }

        public void CreateStation(StationDTO stationDTO)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                var station = Mapper.Map<Station>(stationDTO);
                stationCreateQuery.Filter = new StationFilter
                {
                    Name = stationDTO.Name,
                    Town = stationDTO.Town
                };
                var existedSameStation = stationCreateQuery.Execute();
                if (existedSameStation != null && existedSameStation.Count != 0)
                {
                    throw new ArgumentException("This station already exists");
                }
                stationRepository.Insert(station);
                uow.Commit();
            }
        }

        public void DeleteStation(int stationID)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                stationInRouteStationQuery.Filter = new StationFilter { Id = stationID };
                var routeStationsOfStation = stationInRouteStationQuery.Execute();
                if (routeStationsOfStation != null && routeStationsOfStation.Count != 0)
                {
                    throw new ArgumentException();
                }
                stationRepository.Delete(stationID);
                uow.Commit();
            }
        }

        public StationDTO GetStationById(int stationID)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                var station = stationRepository.GetById(stationID);
                if(station == null)
                {
                    return null;
                }
                else
                {
                    return Mapper.Map<StationDTO>(station);
                }
            }
        }

        public List<StationDTO> GetAllStationsByTown(string town)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                stationListQuery.Filter = new StationFilter { Town = town };
                return stationListQuery.Execute().ToList();
            }
        }

        public List<StationDTO> GetStationsByFilter(StationFilter filter)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                stationListQuery.Filter = filter;
                return stationListQuery.Execute().ToList();
            }
        }
    }
}
