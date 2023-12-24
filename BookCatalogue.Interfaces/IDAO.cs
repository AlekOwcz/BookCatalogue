using BookCatalogue.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IBook> GetAllBooks();
        IEnumerable<IAuthor> GetAllAuthors();

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
