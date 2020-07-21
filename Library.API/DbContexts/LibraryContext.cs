using Library.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.DbContexts
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base (options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopies> BookCopies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        public DbSet<Entities.Library> Libraries { get; set; }
        public DbSet<Publisher> Publisheres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "First Book",
                    YearOfIssue = DateTime.Now,
                    NumberOfPages = 300,
                    PublisherId = 1
                });
            modelBuilder.Entity<BookCopies>().HasData(
                new BookCopies
                {
                    Id = 1,
                    NumberOfCopies = 10,
                    BookId = 1,
                    LibraryId = 1
                });
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Name = "Vlatko",
                    Phone = "222"
                });
            modelBuilder.Entity<Lending>().HasData(
                new Lending
                {
                    Id = 1,
                    BookId = 1,
                    ClientId = 1,
                    WitdrawDate = DateTime.Now,
                }) ;
            modelBuilder.Entity<Entities.Library>().HasData(
                new Entities.Library
                {
                    Id = 1,
                    Name = "First Library",
                    Address = "Partizanska bb",
                    City = "Skopje"

                });
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher
                {
                    Id = 1,
                    Country = "Macedonia",
                    Name = "Tri"
                });
        }
    }
}
