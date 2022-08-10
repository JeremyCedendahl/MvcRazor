using MvcRazorLabb.Models;
using MvcRazorLabb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kurs_ASP.NET_Laboration_4_MVC_Razor.Controllers
{
    public class LoanBookController : Controller
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;

        public LoanBookController(IBorrowedBookRepository BorrowedBookRepository, ICustomerRepository customerRepository, IBookRepository bookRepository)
        {
            _borrowedBookRepository = BorrowedBookRepository;
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
             
            return View();
        }
        public IActionResult Form()
        {
            return View();
        }
        public IActionResult ReturnBook(int id)
        {
            var chosenBook = _borrowedBookRepository.GetLoanById(id);
            if (chosenBook == null)
            {
                return NotFound();
            }
            return View(chosenBook);
        }
        public IActionResult BookReturned(Book book)
        {
            var returned = _bookRepository.GetBookById(book.Id);
            return View(returned);
        }
        public IActionResult BookLoan(BorrowedBook borrowedBook)
        {

            var bookLoan = new CustomerBookViewModel
            {
                Customer = _customerRepository.GetCustomer(borrowedBook.CustomerId),
                Book = _bookRepository.GetBookById(borrowedBook.BookId)
            };
            return View(bookLoan);

        }
        public IActionResult AllLoans()
        {
            var loanBookViewModel = new LoanBookViewModel
            {
                Customers = _customerRepository.GetAllCustomers,
                Books = _bookRepository.GetAll,
                BorrowedBooks = _borrowedBookRepository.GetAllLoans
            };
            if (loanBookViewModel == null)
            {
                return NotFound();
            }
            return View(loanBookViewModel);
        }

        [HttpPost]
        [Route("LoanBook/Add")]
        public async Task<ActionResult<BorrowedBook>> AddLoan(BorrowedBook borrowedBook)
        {
            try
            {
                if (borrowedBook == null)
                {
                    return BadRequest();
                }
                var book = _bookRepository.GetBookById(borrowedBook.BookId);
                if (book.IsAvailable == true)
                {
                    var newLoan = await _borrowedBookRepository.AddLoan(borrowedBook);
                    var updateBook = _bookRepository.GetBookById(borrowedBook.BookId);
                    var loantaker = _customerRepository.GetCustomer(borrowedBook.CustomerId);
                    _bookRepository.UpdateBook(updateBook);
                    return RedirectToAction("BookLoan", newLoan);
                }
                return RedirectToAction("Form");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, failed to Create a new object to Database...");
            }
        }

        [HttpPost]
        public ActionResult ReturnLoan(BorrowedBook borrowedBook)
        {
            try
            {
                if (borrowedBook == null)
                {
                    return BadRequest();
                }
                var loan = _borrowedBookRepository.GetLoanById(borrowedBook.Id);
                var returnLoan = _borrowedBookRepository.UpdateLoan(loan);
                var updateBook = _bookRepository.GetBookById(returnLoan.BookId);
                _bookRepository.UpdateBook(updateBook);
                return RedirectToAction("BookReturned", updateBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, failed to Create a new object to Database...");
            }
        }
        [Route("LoanBook/Test")]
        public string Test(BorrowedBook borrowedBook)
        {
            return $"Låntagare: {borrowedBook.CustomerId} Bok: {borrowedBook.BookId} Start: {borrowedBook.StartDate} Slut: {borrowedBook.EndDate} Bokåterlämnad: {borrowedBook.IsReturned}";
        }
    }
}
