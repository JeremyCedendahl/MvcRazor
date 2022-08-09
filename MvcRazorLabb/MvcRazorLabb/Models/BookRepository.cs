using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
    public class BookRepository : IBookRepository
    {
        private DatabaseContext _dbContext;

        public BookRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetAll
        {
            get
            {
                return _dbContext.Books;
            }
        }

        public IEnumerable<Book> GetAvailibleBooks
        {
            get
            {
                return _dbContext.Books.Where(b => b.IsAvailable == true);
            }
        }


        public Book GetBookById(int id)
        {
            return _dbContext.Books.FirstOrDefault(b => b.Id == id);
        }

        public Book UpdateBook(Book book)
        {
            var result = _dbContext.Books.Find(book.Id);

            if (result != null)
            {
                if (result.IsAvailable == true)
                {
                    result.IsAvailable = false;
                    _dbContext.Books.Update(result);
                    _dbContext.SaveChanges();
                    return result;
                }
                else if (book.IsAvailable == false)
                {
                    result.IsAvailable = true;
                    _dbContext.Books.Update(result);
                    _dbContext.SaveChanges();
                    return result;
                }
            }
            return book;

        }
    }
}
