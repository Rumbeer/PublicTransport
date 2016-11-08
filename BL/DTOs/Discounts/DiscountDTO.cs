using DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Discounts
{
    public class DiscountDTO
    {
        public int ID { get; set; }
        [Range(0, 100)]
        [Required]
        public int Value { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        [MaxLength(15)]
        public string Code { get; set; }
    }
}
