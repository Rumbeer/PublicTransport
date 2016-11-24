using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs;

namespace API.Models
{
    public class SpecificLinkModel
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public DateTime? DepartFromFirstStation { get; set; }
        public string NameOfFirstStation { get; set; }
        public string NameOfLastStation { get; set; }
    }
}