using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Companies;
using BL.DTOs.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class CompanyListQuery : AppQuery<CompanyDTO>
    {
        public CompanyFilter Filter { get; set; }

        public CompanyListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<CompanyDTO> GetQueryable()
        {
            IQueryable<Company> query = Context.Companies;

            if (!string.IsNullOrEmpty(Filter?.Name))
            {
                query = query.Where(company => company.Name.ToLower().Equals(Filter.Name.ToLower()));
            }

            if (Filter?.TimeToRedeem != null)
            {
                query = query.Where(company => company.TimeToRedeem > Filter.TimeToRedeem);
            }

            return query.ProjectTo<CompanyDTO>();
        }
    }
}