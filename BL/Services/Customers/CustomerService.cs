using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Customers;
using BL.DTOs.Filters;
using BL.Queries;
using BL.Repositories;
using BL.Repositories.UserAccount;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;


namespace BL.Services.Customers
{
    public class CustomerService : AppService, ICustomerService
    {
        public int CustomerPageSize => 1;

        #region Dependencies
        private readonly UserAccountRepository userRepository;

        private readonly CustomerRepository customerRepository;

        private readonly CustomerListAllQuery customerListAllQuery;

        private readonly CustomerListQuery customerListQuery;

        public CustomerService(UserAccountRepository userRepository, CustomerRepository customerRepository, CustomerListAllQuery customerListAllQuery, CustomerListQuery customerListQuery)
        {
            this.userRepository = userRepository;
            this.customerRepository = customerRepository;
            this.customerListAllQuery = customerListAllQuery;
            this.customerListQuery = customerListQuery;
        }
        #endregion

        public void CreateCustomer(Guid customerAccountId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var customerAccount = userRepository.GetById(customerAccountId);

                var customer = new Customer { Account = customerAccount };

                customerRepository.Insert(customer);

                uow.Commit();
            }
        }

        public void EditCustomer(CustomerDTO customerDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var customer = customerRepository.GetById(customerDto.ID);
                Mapper.Map(customerDto, customer);
                customerRepository.Update(customer);
                uow.Commit();
            }
            throw new NotImplementedException();
        }

        public CustomerDTO GetCustomerById(int customerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var customer = customerRepository.GetById(customerId, cust => cust.Account);
                return customer == null ? null : Mapper.Map<CustomerDTO>(customer);
            }
        }

        public CustomerListQueryResultDTO ListAllCustomers(int requestedPage = 1)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = customerListAllQuery;
                query.ClearSortCriterias();
                query.Skip = Math.Max(0, requestedPage - 1) * CustomerPageSize;
                query.Take = CustomerPageSize;
                query.AddSortCriteria(customer => customer.LastName, SortDirection.Ascending);
                return new CustomerListQueryResultDTO
                {
                    RequestedPage = requestedPage,
                    TotalResultCount = query.GetTotalRowCount(),
                    ResultsPage = query.Execute()
                };
            }
        }

        public CustomerListQueryResultDTO ListCustomersByFilter(CustomerFilter filter, int requestedPage = 1)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = customerListQuery;
                query.ClearSortCriterias();
                query.Filter = filter;
                query.Skip = Math.Max(0, requestedPage - 1) * CustomerPageSize;
                query.Take = CustomerPageSize;
                query.AddSortCriteria(customer => customer.LastName, SortDirection.Ascending);
                return new CustomerListQueryResultDTO
                {
                    RequestedPage = requestedPage,
                    TotalResultCount = query.GetTotalRowCount(),
                    ResultsPage = query.Execute()
                };
            }
        }

        public CustomerDTO GetCustomerByEmail(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentException("Customer service - GetCustomerByEmail(...) Email cannot be empty or null");
                }
                var query = customerListQuery;
                query.ClearSortCriterias();
                query.Filter = new CustomerFilter { Email = email };
                return query.Execute().SingleOrDefault();
            }
        }
    }
}
