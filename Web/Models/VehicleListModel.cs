using X.PagedList;
using BL.DTOs.Vehicles;

namespace Web.Models
{
    public class VehicleListModel
    {
        public int CompanyId { get; set; }
        public IPagedList<VehicleDTO> Vehicles { get; set; }
    }
}