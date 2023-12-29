using BookCatalogue.Core;
using BookCatalogue.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Interfaces
{
    public interface IDAO
    {
        Task<int> SaveChangesAsync();

        IAuthor ConvertToIAuthor(AuthorDTO authorDTO);
        IBook ConvertToIBook(BookDTO bookDTO);

        bool AuthorExists(Guid id);
        bool BookExists(Guid id);

        IEnumerable<IBook> GetAllBooks();
        Task<IEnumerable<IBook>> GetAllBooksAsync();
        IEnumerable<IAuthor> GetAllAuthors();
        Task<IEnumerable<IAuthor>> GetAllAuthorsAsync();

        IAuthor? GetAuthor(Guid id);
        Task<IAuthor?> GetAuthorAsync(Guid id);
        IBook? GetBook(Guid id);
        Task<IBook?> GetBookAsync(Guid id);

        IAuthor CreateNewAuthor(string name, string surname, DateTime dateOfBirth);
        IBook CreateNewBook(string title, int releaseYear, IAuthor author, Language language, Genre genre);

        void DeleteAuthor(IAuthor author);
        void DeleteBook(IBook book);

        void AddAuthor(IAuthor author);
        void AddBook(IBook book);

        void UpdateAuthor(IAuthor author);
        void UpdateBook(IBook book);

    }
}
