using BL.DTOs.Companies;
using BL.DTOs.Discounts;
using BL.DTOs.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CompanyDetailsModel
    {
        public CompanyDTO Company { get; set; }
        public IEnumerable<DiscountDTO> Discounts { get; set; }
        public IEnumerable<VehicleDTO> Vehicles { get; set; }
    }
}