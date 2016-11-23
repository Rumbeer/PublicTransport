
using BL.DTOs.Discounts;

namespace Web.Models
{
    public class CreateDiscountModel
    {
        public int CompanyId { get; set; }
        public DiscountDTO Discount { get; set; }    
    }
}