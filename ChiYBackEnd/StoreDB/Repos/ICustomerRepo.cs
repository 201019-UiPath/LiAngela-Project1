using System.Collections.Generic;

using StoreDB.Models;

namespace StoreDB.Repos
{
    public interface ICustomerRepo
    {
         List<Customer> GetAllCustomers();

         int GetLastCustomerId();

         Customer GetCustomerByEmailAddress(string customerEmailAddress);

         void AddCustomer(Customer customer);
    }
}