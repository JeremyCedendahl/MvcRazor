using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        //En bok kan lånas av många Customers
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
