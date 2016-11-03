using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Questionnaire : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public virtual Ticket Ticket { get; set; }
    }
}
