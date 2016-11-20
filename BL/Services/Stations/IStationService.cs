using BL.DTOs;
using BL.DTOs.Filters;
using BL.DTOs.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Stations
{
    public interface IStationService
    {
        void CreateStation(StationDTO stationDTO);

        void DeleteStation(int stationID);

        StationDTO GetStationById(int stationID);

        List<StationDTO> GetAllStationsByTown(string town);

        List<StationDTO> GetStationsByFilter(StationFilter filter);
    }
}
