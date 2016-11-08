using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Discounts;
using BL.DTOs.Filters;
using DAL.Enum;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using System;

namespace BL.Services.Discounts
{
    public class DiscountService : AppService, IDiscountService
    {

        #region Dependencies

        private readonly DiscountRepository discountRepository;

        private readonly CompanyRepository companyRepository;

        private readonly DiscountsOfCompanyQuery discountsOfCompanyQuery;

        public DiscountService(DiscountRepository discountRepository, DiscountsOfCompanyQuery discountsOfCompanyQuery, CompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
            this.discountRepository = discountRepository;
            this.discountsOfCompanyQuery = discountsOfCompanyQuery;
        }
        #endregion

        public void CreateDiscount(DiscountDTO discountDto, int companyId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var company = companyRepository.GetById(companyId);
                if (company == null)
                {
                    throw new NullReferenceException("Discount service - CreateDiscount(...) company cant be null");
                }
                if (discountDto.DiscountType != DiscountType.Special && ListDiscountsOfCompany(discountDto.DiscountType, companyId).FirstOrDefault() != null)
                {
                    throw new NullReferenceException("Discount service - CreateDiscount(...) Company cannot have discounts of same type");
                }
                var discount = Mapper.Map<Discount>(discountDto);

                discount.Company = company;
                discountRepository.Insert(discount);
                uow.Commit();
            }
        }

        public void DeleteDiscount(int discountId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                discountRepository.Delete(discountId);
                uow.Commit();
            }
        }

        public void EditDiscount(DiscountDTO discountDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var discount = discountRepository.GetById(discountDto.ID);
                if(discountDto.DiscountType != discount.DiscountType)
                {
                    throw new ArgumentException("Discount service - EditDiscount(...) discount cannot change type");
                }
                Mapper.Map(discountDto, discount);

                discountRepository.Update(discount);
                uow.Commit();
            }
        }

        public IEnumerable<DiscountDTO> ListDiscountsOfCompany(DiscountType? discountType, int companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = discountsOfCompanyQuery;
                query.ClearSortCriterias();
                query.Filter = new DiscountFilter { DiscountType = discountType, CompanyId = companyId };
                return discountsOfCompanyQuery.Execute() ?? new List<DiscountDTO>();
            }
        }

        public DiscountDTO GetDiscountById(int discountId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var discount = discountRepository.GetById(discountId);
                return discount != null ? Mapper.Map<DiscountDTO>(discount) : null;
            }
        }
    }
}
