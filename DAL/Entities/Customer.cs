using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Customer : IEntity<int>
    {
        public int ID { get; set; }

        public virtual UserAccount Account { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
    }
}
