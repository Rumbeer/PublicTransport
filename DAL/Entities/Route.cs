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
    public class Route : IEntity<int>
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public Company Company { get; set; }

        public virtual List<RouteStation> RouteStations { get; set; }

    }
}
