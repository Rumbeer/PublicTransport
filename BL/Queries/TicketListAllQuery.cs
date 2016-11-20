using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Tickets;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class TicketListAllQuery : AppQuery<TicketDTO>
    {
        public TicketListAllQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<TicketDTO> GetQueryable()
        {
            return Context.Tickets.ProjectTo<TicketDTO>();
        }
    }
}
