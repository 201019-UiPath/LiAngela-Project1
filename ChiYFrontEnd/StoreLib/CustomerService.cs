using System;
using System.Collections.Generic;

using StoreDB.Models;
using StoreDB.Repos;

namespace StoreLib
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepo repo;

        public CustomerService(ICustomerRepo repo)
        {
            this.repo = repo;
        }

        public int GetNewCustomerId()
        {
            return repo.GetLastCustomerId() + 1;
        }

        public List<Customer> GetAllCustomers()
        {
            return repo.GetAllCustomers();
        }

        public Customer GetCustomerByEmailAddress(string customerEmailAddress)
        {
            Customer customer = repo.GetCustomerByEmailAddress(customerEmailAddress);
            if (customer == null)
            {
                throw new Exception("Your email address does not yet exist in our customer database");
            }
            return customer;
        }

        public void AddCustomer(Customer newCustomer)
        {
            newCustomer.CustomerId = GetNewCustomerId();
            repo.AddCustomer(newCustomer);
        }
    }
}
