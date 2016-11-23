using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Filters
{
    public class RouteStationFilter
    {
        public int? RouteId { get; set; }
        public int? DepartStationId { get; set; }
        public int? ArrivalStationId { get; set; }
        public DateTime? DepartFromFirstStation { get; set; }
        public int? Order { get; set; }
    }
}