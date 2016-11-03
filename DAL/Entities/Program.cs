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
    public class Program : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public DateTime Depart { get; set; }

        [Required]
        public DateTime Arrive { get; set; }

        public bool IsSeatOccupied { get; set; }

        [Required]
        public virtual RouteStation RouteStation { get; set; }

        [Required]
        public virtual Seat Seat { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
