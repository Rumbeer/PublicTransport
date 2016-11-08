using BL.DTOs.Filters;

namespace BL.DTOs.Vehicles
{
    /// <summary>
    /// Wrapper for product list query result with paging related data
    /// </summary>
    public class VehicleListQueryResultDTO : PagedListQueryResultDTO<VehicleDTO>
    {
        /// <summary>
        /// Filter used in this query
        /// </summary>
        public VehicleFilter Filter { get; set; }
    }
}