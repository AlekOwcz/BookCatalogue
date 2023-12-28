using BookCatalogue.Core;
using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookCatalogue.DAOSQL
{
    public class DAOSQL : IDAO
    {
        private DataContext _context;

        public DAOSQL()
        {
            _context = InitializeContext();
        }

        private static DataContext InitializeContext()
        {

            var builder = new ConfigurationBuilder()
               .AddJsonFile("dbconfig.json");

            return new DataContext(builder.Build());
        }

        public void AddAuthor(IAuthor author)
        {
            if (author is BO.Author authorEntity)
            {
                _context.Authors.Add(authorEntity);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The author must be of type BookCatalogue.DAOSQL.BO.Author.");
            }
        }

        public void AddBook(IBook book)
        {
            if (book is BO.Book bookEntity)
            {
                _context.Books.Add(bookEntity);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The book must be of type BookCatalogue.DAOSQL.BO.Book.");
            }
        }

        public IAuthor CreateNewAuthor(string name, string surname, DateTime dateOfBirth)
        {
            BO.Author author = new BO.Author();
            author.Name = name;
            author.Surname = surname;
            author.DateOfBirth = dateOfBirth;
            return author;
        }

        public IBook CreateNewBook(string title, int releaseYear, IAuthor author, Language language, Genre genre)
        {
            BO.Book book = new BO.Book();
            book.Title = title;
            book.ReleaseYear = releaseYear;
            if (author is BO.Author bookEntity)
            {
                book.Author = bookEntity;
            }
            else
            {
                throw new InvalidCastException("Author must inherit from BookCatalogue.DAOSQL.BO.Author");
            }
            book.Language = language;
            book.Genre = genre;
            return book;
        }

        public void DeleteAuthor(IAuthor author)
        {
            var authorToDelete = _context.Authors.FirstOrDefault(a => a.ID == author.ID);
            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Author not found in the database.");
            }
        }

        public void DeleteBook(IBook book)
        {
            var bookToDelete = _context.Books.FirstOrDefault(b => b.ID == book.ID);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Book not found in the database.");
            }
        }

        public IEnumerable<IAuthor> GetAllAuthors()
        {
            return _context.Authors;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _context.Books;
        }

        public void UpdateAuthor(IAuthor author)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.ID == author.ID);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.Surname = author.Surname;
                existingAuthor.DateOfBirth = author.DateOfBirth;

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Author not found in the database.");
            }
        }

        public void UpdateBook(IBook book)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.ID == book.ID);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                if (book is BO.Book bookEntity)
                {
                    book.Author = bookEntity.Author;
                }
                else
                {
                    throw new InvalidCastException("Book must inherit from BookCatalogue.DAOSQL.BO.Book");
                }
                existingBook.Language = book.Language;
                existingBook.ReleaseYear = book.ReleaseYear;
                existingBook.Genre = book.Genre;

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Book not found in the database.");
            }
        }
    }
}
