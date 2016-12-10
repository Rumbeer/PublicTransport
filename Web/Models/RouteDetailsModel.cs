using BL.DTOs.RouteStations;
using System;
using System.Collections.Generic;


namespace Web.Models
{
    public class RouteDetailsModel
    {
        public List<DateTime> DepartTimes { get; set; }
        public List<RouteStationDTO> Templates { get; set; }
        public int CompanyId { get; set; }
        public int RouteId { get; set; }
    }
}