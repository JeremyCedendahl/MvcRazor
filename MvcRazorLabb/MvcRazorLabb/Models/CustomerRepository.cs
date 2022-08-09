using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _dbContext;

        public CustomerRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Customer> GetAllCustomers
        {
            get
            {
                return _dbContext.Customers
                    .OrderBy(l => l.LastName)
                    .ThenBy(f => f.FirstName)
                    .Include(b => b.BorrowedBooks)
                    .ThenInclude(b => b.Book);
            }
        }


        public async Task<Customer> AddCustomer(Customer newCustomer)
        {
            var customer = await _dbContext.Customers.AddAsync(newCustomer);
            await _dbContext.SaveChangesAsync();
            return customer.Entity;
        }

        public Customer DeleteCustomer(Customer customerToDelete)
        {
            _dbContext.Customers.Remove(customerToDelete);
            _dbContext.SaveChanges();
            return customerToDelete;
        }

        public IEnumerable<BorrowedBook> GetBooks(int id)
        {
            return _dbContext.BorrowedBooks.Where(c => c.CustomerId == id);
        }

        public Customer GetCustomer(int id)
        {
            return _dbContext.Customers
                .Include(b => b.BorrowedBooks)
                .ThenInclude(b => b.Book)
                .FirstOrDefault(i => i.Id == id);
        }

        public Customer UpdateCustomer(Customer customerToUpdate)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerToUpdate.Id);
            if (customer != null)
            {
                customer.FirstName = customerToUpdate.FirstName;
                customer.LastName = customerToUpdate.LastName;
                customer.Email = customerToUpdate.Email;
                _dbContext.Customers.Update(customer);
                _dbContext.SaveChanges();
            }
            return customer;
        }
    }
}
