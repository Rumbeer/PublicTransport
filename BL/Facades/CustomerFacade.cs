using BL.DTOs.Customers;
using BL.DTOs.Filters;
using BL.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class CustomerFacade
    {
        private readonly ICustomerService customerService;
        public CustomerFacade(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        /// <summary>
        /// Creates new customer
        /// </summary>
        /// <param name="customerDto">customer details</param>
        void CreateCustomer(CustomerDTO customerDto)
        {
            customerService.CreateCustomer(customerDto);
        }

        /// <summary>
        /// Updates customer
        /// </summary>
        /// <param name="companyDto">cusotmer details</param>
        void EditCustomer(CustomerDTO customerDto)
        {
            customerService.EditCustomer(customerDto);
        }

        /// <summary>
        /// Deletes customer by id
        /// </summary>
        /// <param name="customerId">id of customer</param>
        void DeleteCustomer(int customerId)
        {
            customerService.DeleteCustomer(customerId);
        }

        /// <summary>
        /// Gets companyDTO of specific id
        /// </summary>
        /// <param name="customerDto">id of customer to be returned</param>
        /// <returns>Customer which has customerId</returns>
        CustomerDTO GetCustomerById(int customerId)
        {
            return customerService.GetCustomerById(customerId);
        }

        /// <summary>
        /// Lists all customers in requested page
        /// </summary>
        /// <param name="requestedPage">requested page</param>
        /// <returns>Page of customers</returns>
        CustomerListQueryResultDTO ListAllCustomers(int requestedPage)
        {
            return customerService.ListAllCustomers(requestedPage);
        }

        /// <summary>
        /// Gets customer of given email
        /// </summary>
        /// <param name="email">email of customer</param>
        /// <returns></returns>
        CustomerDTO GetCustomerByEmail(string email)
        {
            return customerService.GetCustomerByEmail(email);
        }

        /// <summary>
        /// Gets customers that meet criterias given in filter
        /// </summary>
        /// <param name="filter">filter by which filters</param>
        /// <param name="requestedPage">page to be given</param>
        /// <returns></returns>
        CustomerListQueryResultDTO ListCustomersByFilter(CustomerFilter filter, int requestedPage)
        {
            return customerService.ListCustomersByFilter(filter, requestedPage);
        }
    }
}
