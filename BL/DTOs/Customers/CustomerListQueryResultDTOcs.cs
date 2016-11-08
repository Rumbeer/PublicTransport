using BL.DTOs.Filters;

namespace BL.DTOs.Customers
{
    /// <summary>
    /// Wrapper for product list query result with paging related data
    /// </summary>
    public class CustomerListQueryResultDTO : PagedListQueryResultDTO<CustomerDTO> { }
}