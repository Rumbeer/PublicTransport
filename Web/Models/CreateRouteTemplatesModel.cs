using BL.DTOs.RouteStations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CreateRouteTemplatesModel
    {
        public string StationName { get; set; }

        public string StationTown { get; set; }

        public int CompanyId { get; set; }

        public int RouteId { get; set; }

        public string TimeToNextStation { get; set; }

        public string TimeFromFirstStation { get; set; }

        public RouteStationDTO RouteStation { get; set; }
    }
}