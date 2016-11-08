using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Company : IEntity<int>
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public double CostPerKm { get; set; }

        [NotMapped]
        public bool RedeemableTicket => this.TimeToRedeem != null;

        public TimeSpan? TimeToRedeem { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }
        //[ForeignKey("Tariff")]
        //public int TariffId { get; set; }

        public virtual List<Discount> Discounts { get; set; }
    }
}
