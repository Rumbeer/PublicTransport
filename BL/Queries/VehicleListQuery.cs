using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Vehicles;
using BL.DTOs.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class VehicleListQuery : AppQuery<VehicleDTO>
    {
        public VehicleFilter Filter { get; set; }

        public VehicleListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<VehicleDTO> GetQueryable()
        {
            IQueryable<Vehicle> query = Context.Vehicles.Include(nameof(Vehicle.Company));

            if(Filter?.CompanyId != null && Filter?.CompanyId > 0)
            {
                query = query.Where(vehicle => vehicle.Company.ID == Filter.CompanyId);
            }

            if (!string.IsNullOrEmpty(Filter?.LicencePlate))
            {
                query = query.Where(vehicle => vehicle.LicencePlate.ToLower().Equals(Filter.LicencePlate.ToLower()));
            }

            if (!string.IsNullOrEmpty(Filter?.VehicleBrand))
            {
                query = query.Where(vehicle => vehicle.VehicleBrand.ToLower().Equals(Filter.VehicleBrand.ToLower()));
            }

            if(Filter?.VehicleType != null)
            {
                query = query.Where(vehicle => vehicle.VehicleType == Filter.VehicleType);
            }

            return query.ProjectTo<VehicleDTO>();
        }
    }
}
