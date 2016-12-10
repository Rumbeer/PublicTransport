
using System;

namespace Web.Models
{
    public class CreateSpecificRouteModel
    {
        public string DepartTime { get; set; }

        public int RouteId { get; set; }

        public int VehicleId { get; set; }

        public int CompanyId { get; set; }
    }
}