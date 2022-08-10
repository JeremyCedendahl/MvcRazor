using MvcRazorLabb.Models;
using MvcRazorLabb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult List()
        {
            var listOfCustomers = new CustomerListViewModel();
            listOfCustomers.Customers = _customerRepository.GetAllCustomers;
            return View(listOfCustomers);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var singleCustomer = _customerRepository.GetCustomer(id);
            if (singleCustomer == null)
            {
                return NotFound();
            }
            return View(singleCustomer);
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Edited(Customer customer)
        {
            return View(customer);
        }
        public IActionResult Deleted(Customer customer)
        {
            return View(customer);
        }
        public IActionResult Added(Customer customer)
        {
            return View(customer);
        }
        public IActionResult EditForm(int id)
        {
            var customerToEdit = _customerRepository.GetCustomer(id);
            return View(customerToEdit);
        }
        public IActionResult DeleteForm(int id)
        {
            var customerToDelete = _customerRepository.GetCustomer(id);
            return View(customerToDelete);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var deletedCustomer = _customerRepository.DeleteCustomer(customer);
                return RedirectToAction("Deleted", deletedCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, failed to Create a new object to Database...");
            }
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var customerToEdit = _customerRepository.UpdateCustomer(customer);
                return RedirectToAction("Edited", customerToEdit);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, failed to Create a new object to Database...");
            }
        }

        [HttpPost]
        [Route("Customer/Add")]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var newCustomer = await _customerRepository.AddCustomer(customer);

                return RedirectToAction("Added", newCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, failed to Create a new object to Database...");
            }
        }
    }
}
