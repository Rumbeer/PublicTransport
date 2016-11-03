using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class RouteStation : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public double DistanceFromPreviousStation { get; set; }

        [Required]
        public virtual Route  Route { get; set; }

        [Required]
        public virtual Station Station { get; set; }

        public virtual List<Program> Programs { get; set; }
    }
}
