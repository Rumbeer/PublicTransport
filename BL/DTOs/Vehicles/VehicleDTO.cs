using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace BL.DTOs.Vehicles
{
    public class VehicleDTO
    {
        public int ID { get; set; }

        public string VehicleBrand { get; set; }

        public string LicencePlate { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public int SeatCount { get; set; }
    }
}
