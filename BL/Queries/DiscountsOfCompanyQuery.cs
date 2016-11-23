using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Discounts;
using BL.DTOs.Filters;
using DAL.Entities;
using DAL.Enum;
using Riganti.Utils.Infrastructure.Core;
namespace BL.Queries
{
    public class DiscountsOfCompanyQuery : AppQuery<DiscountDTO>
    {
        public DiscountFilter Filter { get; set; }

        public DiscountsOfCompanyQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<DiscountDTO> GetQueryable()
        {
            IQueryable<Discount> query = Context.Discounts.Include(nameof(Discount.Company));
            if (Filter?.CompanyId != null && Filter?.CompanyId > 0)
            {
                query = query.Where(discount => discount.Company.ID == Filter.CompanyId);
            }
            else
            {
                throw new ArgumentException("DiscountOfCompanyQuery - GetQueryable(...) Company cannot be null");
            }
            if(Filter?.DiscountType != null)
            {
                query = query.Where(discount => discount.DiscountType == (DAL.Enum.DiscountType)Filter.DiscountType);
            }
            return query.ProjectTo<DiscountDTO>();
        }
    }
}
