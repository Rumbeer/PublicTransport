using System;
using DAL.Enum;

namespace BL.DTOs.Filters
{
    public class DiscountFilter
    {
        public int? CompanyId { get; set; }

        public DiscountType? DiscountType { get; set; }
    }
}

