using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Seats
{
    public class SeatDTO
    {
        public int ID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please set the value greater than 0")]
        public int SeatNumber { get; set; }
    }
}
