using MvcRazorLabb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.ViewModels
{
    public class AvailibleBooksViewModel
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
