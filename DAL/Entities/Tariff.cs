using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Tariff : IEntity<int>
    {
        public int ID { get; set; }
        [Required]
        public double CostPerKm { get; set; }

        [Required]
        public virtual Company Company { get; set; }
        public virtual List<Discount> Discounts { get; set; }
    }
}
