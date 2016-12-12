using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Customers;
using BL.DTOs.Filters;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using BL.Repositories.UserAccount;

namespace BL.Services.Customers
{
    public class CustomerService : AppService, ICustomerService
    {
        public int CustomerPageSize => 1;

        #region Dependencies
        private readonly CustomerRepository customerRepository;

        private readonly CustomerListAllQuery customerListAllQuery;

        private readonly CustomerListQuery customerListQuery;

        private readonly UserAccountRepository userAccountRepository;

        public CustomerService(CustomerRepository customerRepository, CustomerListAllQuery customerListAllQuery, CustomerListQuery customerListQuery,
                                    UserAccountRepository userAccountRepository)
        {
            this.customerRepository = customerRepository;
            this.customerListAllQuery = customerListAllQuery;
            this.customerListQuery = customerListQuery;
            this.userAccountRepository = userAccountRepository;
        }
        #endregion

        /// <summary>
        /// Creates new customer (user account must be created first)
        /// </summary>
        /// <param name="userAccountId">Customer user account ID</param>
        public void CreateCustomer(Guid userAccountId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var customerAccount = userAccountRepository.GetById(userAccountId);

                var customer = new Customer { Account = customerAccount };

                customerRepository.Insert(customer);

                uow.Commit();
            }
        }

        public void CreateCustomer(CustomerDTO customerDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var customer = Mapper.Map<Customer>(customerDto);
                customerRepository.Insert(customer);
                uow.Commit();
            }
            var c = GetCustomerByEmail(customerDto.Email);
        }

        public void EditCustomer(CustomerDTO customerDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var customer = customerRepository.GetById(customerDto.ID, c => c.Tickets);
                Mapper.Map(customerDto, customer);
                customerRepository.Update(customer);
                uow.Commit();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                customerRepository.Delete(customerId);
                uow.Commit();
            }
        }

        public CustomerDTO GetCustomerById(int customerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var customer = customerRepository.GetById(customerId);
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
                query.Skip = 0;
                query.AddSortCriteria(customer => customer.LastName, SortDirection.Ascending);
                query.Take = 1;
                return query.Execute().SingleOrDefault();
            }
        }
    }
}
