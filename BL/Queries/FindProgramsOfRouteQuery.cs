using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.DTOs.Programs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class FindProgramsOfRouteStationQuery : AppQuery<ProgramDTO>
    {
        public ProgramFilter Filter;
        public FindProgramsOfRouteStationQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<ProgramDTO> GetQueryable()
        {
            IQueryable<Program> query = Context.Programs;
            query = query.Where(program => program.RouteStation.ID == Filter.RouteStationID);
            return query.ProjectTo<ProgramDTO>();
        }
    }
}
