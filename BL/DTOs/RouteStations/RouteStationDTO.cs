using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.RouteStations
{
    public class RouteStationDTO
    {
        public int ID { get; set; }

        public DateTime? DepartFromFirstStation { get; set; }

        public TimeSpan Delay { get; set; }

        //zo stanice do dalsej
        [Required]
        public TimeSpan TimeToNextStation { get; set; }

        [Required]
        public TimeSpan TimeFromFirstStation { get; set; }

        public int Order { get; set; }

        [Required]
        public double DistanceFromPreviousStation { get; set; }

    }
}
