using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface ILibraryRepository
    {
        ICollection<Entities.Library> GetLibraries();
        Entities.Library GetLibrary(int id);
        void CreateLibrary(Entities.Library library);
        void UpdateLibrary(Entities.Library library);
        void DeleteLibrary(Entities.Library library);
        bool LibraryExists(int id);

        ICollection<Client> GetClients(string name);
        Client GetClient(int id);
        void AddClient(Client client);
        void UpdateClient(Client client);
        void RemoveClient(Client client);
        bool ClientExists(int id);
        bool Save();

        ICollection<Publisher> GetPublishers();
        Publisher GetPublisher(int id);
        void AddPublisher(Publisher publisher);
        void DeletePublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        bool PublisherExists(int id);
        ICollection<Book> GetBooks(int publisherId);
        Book GetBook(int publisherId, int id);
        void AddBook(int publisherId, Book book);
        void DeleteBook(Book book);
        void UpdateBook(Book book);
        bool BookExists(int id);
        ICollection<BookCopies> GetBookCopiesForBook(int bookId);
        BookCopies GetBookCopyForBook(int bookId, int copyId);
        ICollection<BookCopies> GetBookCopiesForLibrary(int libraryId);
        BookCopies GetBookCopyForLibrary(int libraryId, int copyId);
        void CreateBookCopies(int libraryId, BookCopies bookCopies);
        void UpdateBookCopies(BookCopies bookCopies);
        void DeleteBookCopies(BookCopies bookCopies);
        bool BookCopiesForBookExist(int bookId, int libraryId);


        ICollection<Lending> GetLendings(int clientId);
        Lending GetLending(int clientId, int id);
        void CreateLending(int clientId, Lending lending);
        void UpdateLending(Lending lending);
        void DeleteLending(Lending lending);


    }
}
