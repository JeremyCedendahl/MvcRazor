using MvcRazorLabb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.ViewModels
{
    public class CustomerBookViewModel
    {
        public Book Book { get; set; }

        public Customer Customer { get; set; }
    }
}
