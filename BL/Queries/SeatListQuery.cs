using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using BL.DTOs.Seats;

namespace BL.Queries
{
    public class SeatListQuery : AppQuery<SeatDTO>
    {
        public SeatFilter Filter { get; set; }

        public SeatListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<SeatDTO> GetQueryable()
        {
            IQueryable<Seat> query = Context.Seats.Include(nameof(Seat.Vehicle));
            if(Filter?.VehicleId > 0)
            {
                query = query.Where(seat => seat.Vehicle.ID == Filter.VehicleId);
            }
            
            return query.ProjectTo<SeatDTO>();
        }
    }
}