using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Ticket :IEntity<int>
    {
        public int ID { get; set; }

        public DateTime Departure { get; set; }

        public string RouteName { get; set; }

        public double TotalDistance { get; set; }

        public double Price { get; set; }

        public int SeatNumber { get; set; }

        public int VehicleId { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsRefunded { get; set; }

        public virtual List<Program> Programs { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        public virtual Discount Discount { get; set; }

        [Required]
        public virtual Company Company { get; set; }

        //[ForeignKey("Questionnaire")]
        //public int QuestionnaireId { get; set; }
        //public virtual Questionnaire Questionnaire { get; set; }
    }
}
