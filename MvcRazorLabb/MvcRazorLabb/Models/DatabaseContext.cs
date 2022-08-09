using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRazorLabb.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Data Books
            modelBuilder.Entity<Book>().HasData(new Book { Id = 1, Title = "Sagan om ringen", Author = "J.R.R Tolkien", IsAvailable = true});
            modelBuilder.Entity<Book>().HasData(new Book { Id = 2, Title = "Sagan om de två tornen", Author = "J.R.R Tolkien", IsAvailable = true});
            modelBuilder.Entity<Book>().HasData(new Book { Id = 3, Title = "Sagan om konungens återkomst", Author = "J.R.R Tolkien", IsAvailable = true});
            modelBuilder.Entity<Book>().HasData(new Book { Id = 4, Title = "Alkemisten", Author = "Paulo Coelho", IsAvailable = true });
            modelBuilder.Entity<Book>().HasData(new Book { Id = 5, Title = "Kråkflickan", Author = "Håkan Sundquist och Jerker Eriksson", IsAvailable = true });


            //Seed Data Customer
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 1, FirstName = "Jeremy", LastName = "Cedendahl", Email = "jeremy@jerm.se" });
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 2, FirstName = "Johan", LastName = "Larsson", Email = "johan@johan.se" });
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 3, FirstName = "Douglas", LastName = "Dahlström", Email = "douglas@dogge.se" });
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 4, FirstName = "Sakarias", LastName = "Dahlström", Email = "sakarias@zacke.se" });
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 5, FirstName = "Fia", LastName = "Johansson", Email = "josefine@fia.se" });




        }
    }
}
