using MvcRazorLabb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.ViewModels
{
    public class LoanBookViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<BorrowedBook> BorrowedBooks { get; set; }
    }
}
