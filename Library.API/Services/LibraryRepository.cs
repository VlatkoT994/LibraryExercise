using Library.API.DbContexts;
using Library.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;

        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public void AddBook(int publisherId, Book book)
        {
            var publisher = GetPublisher(publisherId);
            publisher.Books.Add(book);
        }

        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
        }

        public void AddPublisher(Publisher publisher)
        {
            _context.Publisheres.Add(publisher);
        }

        public bool BookCopiesForBookExist(int bookId, int libraryId)
        {
            return _context.BookCopies.Any(b => b.BookId == bookId && b.LibraryId == libraryId);
        }

        public bool BookExists(int id)
        {
            return (_context.Books.Any(b => b.Id == id));
        }

        public bool ClientExists(int id)
        {
            return _context.Clients.Any(c => c.Id == id);
        }

        public void CreateBookCopies(int libraryId, BookCopies bookCopies)
        {
            var library = _context.Libraries.SingleOrDefault(l => l.Id == libraryId);
            library.BookCopies.Add(bookCopies);
        }

        public void CreateLending(int clientId, Lending lending)
        {
            var client = GetClient(clientId);
            client.Lendings.Add(lending);
        }

        public void CreateLibrary(Entities.Library library)
        {
            _context.Libraries.Add(library);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

        public void DeleteBookCopies(BookCopies bookCopies)
        {
            _context.BookCopies.Remove(bookCopies);
        }

        public void DeleteLending(Lending lending)
        {
            _context.Lendings.Remove(lending);
        }

        public void DeleteLibrary(Entities.Library library)
        {
            _context.Libraries.Remove(library);
        }

        public void DeletePublisher(Publisher publisher)
        {
            _context.Publisheres.Remove(publisher);
        }

        public Book GetBook(int publisherId, int id)
        {
            return _context.Books.SingleOrDefault(b => b.Id == id && b.PublisherId == publisherId);
        }

        public ICollection<BookCopies> GetBookCopiesForBook(int bookId)
        {
            return _context.BookCopies.Where(b => b.BookId == bookId).Include(b => b.Library).Include(b => b.Book).ToList();
        }

        public ICollection<BookCopies> GetBookCopiesForLibrary(int libraryId)
        {
            return _context.BookCopies.Where(b => b.LibraryId == libraryId).Include(b => b.Library).Include(b=>b.Book).ToList();
        }

        public BookCopies GetBookCopyForBook(int bookId, int copyId)
        {
            return _context.BookCopies.Include(b => b.Library).Include(b => b.Book).SingleOrDefault(b => b.BookId == bookId && b.Id == copyId);
        }

        public BookCopies GetBookCopyForLibrary(int libraryId, int copyId)
        {
            return _context.BookCopies.Include(b=>b.Library).Include(b => b.Book).SingleOrDefault(b => b.LibraryId == libraryId && b.Id == copyId);
        }

        public ICollection<Book> GetBooks(int publisherId)
        {
            return _context.Books.Where(b => b.PublisherId == publisherId).ToList();
        }

        public Client GetClient(int id)
        {
            return _context.Clients.SingleOrDefault(c => c.Id == id);
        }

        public ICollection<Client> GetClients(string name)
        {
            return _context.Clients.Where(c => c.Name.Contains(name)).ToList();
        }

        public Lending GetLending(int clientId, int id)
        {
            return _context.Lendings.Include(l =>l.Book).SingleOrDefault(l => l.ClientId == clientId && l.Id == id);
        }

        public ICollection<Lending> GetLendings(int clientId)
        {
            return _context.Lendings.Where(l => l.ClientId == clientId).Include(l => l.Book).ToList();
        }

        public ICollection<Entities.Library> GetLibraries()
        {
            return _context.Libraries.Include(c => c.BookCopies).ToList();
        }

        public Entities.Library GetLibrary(int id)
        {
            return _context.Libraries.SingleOrDefault(l => l.Id == id);
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publisheres.SingleOrDefault(p => p.Id == id);
        }

        public ICollection<Publisher> GetPublishers()
        {
            return _context.Publisheres.ToList();
        }

        public bool LibraryExists(int id)
        {
            return (_context.Libraries.Any(l => l.Id == id));
        }

        public bool PublisherExists(int id)
        {
            return (_context.Publisheres.Any(p => p.Id == id));
        }

        public void RemoveClient(Client client)
        {
            _context.Clients.Remove(client);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateBook(Book book)
        {
        }

        public void UpdateBookCopies(BookCopies bookCopies)
        {
        }

        public void UpdateClient(Client client)
        {
        }

        public void UpdateLending(Lending lending)
        {
        }

        public void UpdateLibrary(Entities.Library library)
        {
        }

        public void UpdatePublisher(Publisher publisher)
        {
        }

    }
}
