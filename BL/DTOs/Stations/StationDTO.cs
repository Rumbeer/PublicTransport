using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Stations
{
    public class StationDTO
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Town { get; set; }
    }
}
