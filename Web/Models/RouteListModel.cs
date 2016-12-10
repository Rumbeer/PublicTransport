using BL.DTOs.Routes;
using System.Collections.Generic;
using X.PagedList;

namespace Web.Models
{
    public class RouteListModel
    {
        public int CompanyId { get; set; }
        public Dictionary<int, bool> HasTemplate { get; set; }
        public IPagedList<RouteDTO> Routes { get; set; }
    }
}