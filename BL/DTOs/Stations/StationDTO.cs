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
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Town { get; set; }
        public string ImagePath { get; set; }
    }
}
