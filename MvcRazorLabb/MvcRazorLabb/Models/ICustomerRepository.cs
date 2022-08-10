using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
   public  interface ICustomerRepository
    {
       
        IEnumerable<Customer> GetAllCustomers { get; }

    
        Customer GetCustomer(int id);

       
        Task<Customer> AddCustomer(Customer newCustomer);

        Customer UpdateCustomer(Customer customerToUpdate);

        Customer DeleteCustomer(Customer customerToDelete);

        IEnumerable<BorrowedBook> GetBooks(int id);
    }
}
