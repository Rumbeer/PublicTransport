using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Customers;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class CustomerListAllQuery : AppQuery<CustomerDTO>
    {
        public CustomerListAllQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<CustomerDTO> GetQueryable()
        {
            return Context.Customers
                .Include(nameof(Customer.Tickets))
                .ProjectTo<CustomerDTO>();
        }
    }
}
