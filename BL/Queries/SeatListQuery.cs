using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Companies;
using BL.DTOs.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class SeatListQuery : AppQuery<CompanyDTO>
    {
        public SeatFilter Filter { get; set; }

        public SeatListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<CompanyDTO> GetQueryable()
        {
            IQueryable<Seat> query = Context.Seats;

            query = query.Where(seat => seat.Vehicle.ID == Filter.VehicleId);

            return query.ProjectTo<CompanyDTO>();
        }
    }
}