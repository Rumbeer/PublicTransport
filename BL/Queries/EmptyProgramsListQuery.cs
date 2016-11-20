using System;
using System.Linq;
using BL.AppInfrastructure;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using BL.DTOs.Programs;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class EmptyProgramsListQuery : AppQuery<ProgramDTO>
    {
        public EmptyProgramsListQuery(IUnitOfWorkProvider provider) : base(provider){ }

        public int SeatNumber { get; set; }
        public int RouteStationId { get; set; }

        protected override IQueryable<ProgramDTO> GetQueryable()
        {
            IQueryable<Program> query = Context.Programs.Include(nameof(Program.Seat)).Include(nameof(Program.RouteStation));

            if(SeatNumber > 0 && RouteStationId > 0)
            {
                query = query.Where(program => program.RouteStation.ID == RouteStationId);
                query = query.Where(program => program.Seat.SeatNumber == SeatNumber);
                return query.ProjectTo<ProgramDTO>();
            }
            else
            {
                return null;
            }
        }
    }
}
