using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private DatabaseContext _dbContext;
        public BorrowedBookRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BorrowedBook> GetAllLoans
        {
            get
            {
                return _dbContext.BorrowedBooks
                    .Include(b => b.Book)
                    .Include(c => c.Customer);
            }
        }

        public async Task<BorrowedBook> AddLoan(BorrowedBook borrowedBook)
        {
            var loan = await _dbContext.BorrowedBooks.AddAsync(borrowedBook);
            await _dbContext.SaveChangesAsync();
            return loan.Entity;
        }
        public BorrowedBook GetLoanById(int id)
        {
            return _dbContext.BorrowedBooks.FirstOrDefault(l => l.Id == id);
        }

        public BorrowedBook UpdateLoan(BorrowedBook borrowedBook)
        {
            var loan = _dbContext.BorrowedBooks.FirstOrDefault(loan => loan.Id == borrowedBook.Id);
            if (loan != null)
            {

                loan.IsReturned = true;
                loan.EndDate = loan.EndDate;
                _dbContext.BorrowedBooks.Update(loan);
                _dbContext.SaveChanges();

            }
            return loan;
        }
    }
}
