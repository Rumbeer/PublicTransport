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
    public class Station :IEntity<int>
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Town { get; set; }

        public virtual List<RouteStation> RouteStations { get; set; }
    }
}
