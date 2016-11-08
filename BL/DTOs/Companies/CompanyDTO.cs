using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Companies
{
    public class CompanyDTO
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public double CostPerKm { get; set; }

        public bool RedeemableTicket => this.TimeToRedeem != null;

        public TimeSpan? TimeToRedeem { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}