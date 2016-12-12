
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class CreateSpecificRouteModel
    {
        public string DepartTime { get; set; }

        public int RouteId { get; set; }

        public int VehicleId { get; set; }

        public string LicencePlate { get; set; }

        public int CompanyId { get; set; }

        public List<string> LicencePlates { get; set; }
    }
}