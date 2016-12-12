using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SearchRouteModel
    {
        public string DepartName { get; set; }
        public string DepartTown { get; set; }
        public string ArriveName { get; set; }
        public string ArriveTown { get; set; }

        public bool DepartTime { get; set; }

        public DateTime Time { get; set; }
    }
}