using BL.DTOs.Routes;

namespace Web.Models
{
    public class CreateRouteModel
    {
        public int CompanyId { get; set; }
        public RouteDTO Route { get; set; }
    }
}