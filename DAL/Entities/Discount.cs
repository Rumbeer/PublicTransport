using DAL.Enum;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Discount : IEntity<int>
    {
        public int ID { get; set; }
        [Range(0, 100)]
        [Required]
        public int Value { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        [MaxLength(15)]
        public string Code { get; set; }

        [Required]
        public virtual Tariff Tariff { get; set; }
    }
}
