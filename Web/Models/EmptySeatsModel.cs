using BL.DTOs.Programs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class EmptySeatsModel
    {
        public Dictionary<int,List<ProgramDTO>> Programs { get; set; }
    }
}