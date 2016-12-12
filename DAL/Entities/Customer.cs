using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Relational;

namespace DAL.Entities
{
    public class Customer : IEntity<int>
    {
        public virtual int ID { get; set; }

        [Required]
        public virtual UserAccount Account { get; set; }

        public virtual List<Ticket> Tickets { get; set; }
    }
}
