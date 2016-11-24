using BL.Enum;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Discounts
{
    public class DiscountDTO
    {
        public int ID { get; set; }

        [Range(0, 100)]
        public int Value { get; set; }

        [EnumDataType(typeof(DiscountType), ErrorMessage = "Discount type does not exist within this enum")]
        public DiscountType DiscountType { get; set; }

        [MaxLength(15)]
        public string Code { get; set; }
    }
}
