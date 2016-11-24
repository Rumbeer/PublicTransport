using BL.DTOs.Filters;

namespace BL.DTOs.Routes
{
    /// <summary>
    /// Wrapper for product list query result with paging related data
    /// </summary>
    public class RouteListQueryResultDTO : PagedListQueryResultDTO<RouteDTO>
    {
        /// <summary>
        /// Filter used in this query
        /// </summary>
        public int CompanyId { get; set; }
    }
}