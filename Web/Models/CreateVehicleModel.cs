using BL.DTOs.Vehicles;

namespace Web.Models
{
    public class CreateVehicleModel
    {
        public int CompanyId { get; set; }
        public VehicleDTO Vehicle { get; set; }
    }
}