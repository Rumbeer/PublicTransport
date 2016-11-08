using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Seats
{
    public class SeatDTO
    {
        public int ID { get; set; }

        [Required]
        public int SeatNumber { get; set; }
    }
}
