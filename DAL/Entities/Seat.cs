using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Seat : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public virtual Vehicle Vehicle { get; set;}
    }   
}
