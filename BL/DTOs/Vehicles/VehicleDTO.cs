using System.ComponentModel.DataAnnotations;
using BL.Enum;

namespace BL.DTOs.Vehicles
{
    public class VehicleDTO
    {
        public int ID { get; set; }

        public string VehicleBrand { get; set; }

        public string LicencePlate { get; set; }

        [EnumDataType(typeof(VehicleType), ErrorMessage = "Vehicle type does not exist within this enum")]
        public VehicleType VehicleType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please set the value greater than 0")]
        public int SeatCount { get; set; }
    }
}
