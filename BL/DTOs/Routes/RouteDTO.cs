using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Routes
{
    public class RouteDTO
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
