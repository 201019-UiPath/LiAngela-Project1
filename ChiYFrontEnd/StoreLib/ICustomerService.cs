using StoreDB.Models;
using System.Collections.Generic;

namespace StoreLib
{
    public interface ICustomerService
    {
        int GetNewCustomerId();

        List<Customer> GetAllCustomers();

        Customer GetCustomerByEmailAddress(string customerEmailAddress);
        
        void AddCustomer(Customer newCustomer);
    }
}