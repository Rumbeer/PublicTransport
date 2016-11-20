using DAL.Enum;
using Riganti.Utils.Infrastructure.Core;
using System.Collections.Generic;
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
        public virtual Company Company { get; set; }

        public virtual List<Ticket> Tickets { get; set; }
    }
}
