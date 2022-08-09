using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
   public  interface IBorrowedBookRepository
    {
        IEnumerable<BorrowedBook> GetAllLoans { get; }

        Task<BorrowedBook> AddLoan(BorrowedBook borrowedBook);

        BorrowedBook GetLoanById(int id);

        BorrowedBook UpdateLoan(BorrowedBook borrowedBook);
    }
}
