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
using System.IO;
using System.Drawing;

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

        public string GetStationNameByRouteStation(int routeStationId)
        {
            using (UnitOfWorkProvider.Create())
            {
                stationListQuery.Filter = new StationFilter
                {
                    RouteStationId = routeStationId
                };
                var station = stationListQuery.Execute().ToList().FirstOrDefault();
                if (station != null)
                {
                    return station.Name;
                }
                return null;
            }
        }

        /// <summary>
        /// Method which sets image of specific station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>true, if image does not existed before</returns>
        public bool SetImageOfStation(int stationId, string pathToPhoto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var station = stationRepository.GetById(stationId, s => s.RouteStations);
                var stationDTO = Mapper.Map<StationDTO>(station);
                if(stationDTO.ImagePath != null)
                {
                    return false;
                }
                stationDTO.ImagePath = pathToPhoto;
                Mapper.Map(stationDTO, station);
                stationRepository.Update(station);
                uow.Commit();
                return true;
            }
        }

        public string GetImageOfStation(int stationId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var stationDTO = Mapper.Map<StationDTO>(stationRepository.GetById(stationId));
                if(stationDTO != null)
                {
                    return stationDTO.ImagePath;
                }
                return null;
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
