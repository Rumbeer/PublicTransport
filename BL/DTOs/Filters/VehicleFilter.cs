using DAL.Enum;

namespace BL.DTOs.Filters
{
    public class VehicleFilter
    {
        public int? CompanyId { get; set; }

        public string VehicleBrand { get; set; }

        public string LicencePlate { get; set; }

        public VehicleType? VehicleType { get; set; }
    }
}
