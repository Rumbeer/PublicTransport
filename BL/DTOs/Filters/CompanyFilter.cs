using System;

namespace BL.DTOs.Filters
{
    public class CompanyFilter
    {
        public string Name { get; set; }

        public TimeSpan? TimeToRedeem { get; set; }
    }
}

