using BL.DTOs.RouteStations;
using BL.DTOs.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SearchResultsModel
    {
        public StationDTO DepartStation { get; set; }
        public StationDTO ArriveStation { get; set; }
        public List<Tuple<RouteStationDTO, RouteStationDTO>> Results { get; set; }
    }
}