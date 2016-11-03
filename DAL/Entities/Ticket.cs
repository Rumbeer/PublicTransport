using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Ticket :IEntity<int>
    {
        public int ID { get; set; }

        public virtual List<Program> Programs { get; set; }
        [Required]
        public virtual Tariff Tarrif { get; set; }
        [ForeignKey("Questionnaire")]
        public int QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
    }
}
