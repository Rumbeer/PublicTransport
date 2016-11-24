using BL.DTOs.Routes;
using X.PagedList;

namespace Web.Models
{
    public class RouteListModel
    {
        public int CompanyId { get; set; }
        public IPagedList<RouteDTO> Routes { get; set; }
    }
}