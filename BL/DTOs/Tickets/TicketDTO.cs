using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Tickets
{
    public class TicketDTO
    {
        public int ID { get; set; }

        public DateTime Departure { get; set; }

        public string RouteName { get; set; }

        public double TotalDistance { get; set; }

        public double Price { get; set; }

        public int SeatNumber { get; set; }
        
        public bool IsConfirmed { get; set; }

        public bool IsRefunded { get; set; }
    }
}
