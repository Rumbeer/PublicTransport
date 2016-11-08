using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Customers;
using BL.DTOs.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System.ComponentModel.DataAnnotations;
using System;

namespace BL.Queries
{
    public class CustomerListQuery : AppQuery<CustomerDTO>
    {
        public CustomerFilter Filter { get; set; }

        public CustomerListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<CustomerDTO> GetQueryable()
        {
            IQueryable<Customer> query = Context.Customers.Include(nameof(Customer.Account));

            if (!string.IsNullOrEmpty(Filter?.Email))
            {
                if(!new EmailAddressAttribute().IsValid(Filter?.Email))
                {
                    throw new ArgumentException("CustomerListQuery - GetQueryable(...) given email had invalid format");
                }
                query = query.Where(cutomer => cutomer.Account.Email.ToLower().Equals(Filter.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(Filter?.FirstName))
            {
                query = query.Where(cutomer => cutomer.Account.FirstName.ToLower().Equals(Filter.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(Filter?.LastName))
            {
                query = query.Where(cutomer => cutomer.Account.LastName.ToLower().Equals(Filter.LastName.ToLower()));
            }
            return query.ProjectTo<CustomerDTO>();
        }
    }
}