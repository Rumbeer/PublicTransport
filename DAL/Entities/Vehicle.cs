using Riganti.Utils.Infrastructure.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace DAL.Entities
{
    public class Vehicle : IEntity<int>
    {
        public int ID { get; set; }

        public string VehicleBrand { get; set; }

        public string LicencePlate { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public int SeatCount { get; set; }

        [Required]
        public virtual Company Company { get; set; }
        public virtual List<Seat> Seats { get; set; }
    }
}
