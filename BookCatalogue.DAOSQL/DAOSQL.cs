using BookCatalogue.Core;
using BookCatalogue.Core.DTO;
using BookCatalogue.DAOSQL.BO;
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

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void AddAuthor(IAuthor author)
        {
            if (author is Author authorEntity)
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
            if (book is Book bookEntity)
            {
                _context.Books.Add(bookEntity);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The book must be of type BookCatalogue.DAOSQL.BO.Book.");
            }
        }


        public IAuthor ConvertToIAuthor(AuthorDTO authorDTO)
        {
            IAuthor author = new Author()
            {
                ID = authorDTO.ID,
                Name = authorDTO.Name,
                Surname = authorDTO.Surname,
                DateOfBirth = authorDTO.DateOfBirth
            };

            return author;
        }

        public IBook ConvertToIBook(BookDTO bookDTO)
        {
            IBook book = new Book()
            {
                ID = bookDTO.ID,
                Title = bookDTO.Title,
                ReleaseYear = bookDTO.ReleaseYear,
                Language = bookDTO.Language,
                Genre = bookDTO.Genre
            };
            IAuthor? author = GetAuthor(bookDTO.AuthorID) ?? throw new Exception("Author missing in the database");
            book.Author = author;

            return book;
        }


        public IAuthor CreateNewAuthor(string name, string surname, DateTime dateOfBirth)
        {
            Author author = new Author();
            author.Name = name;
            author.Surname = surname;
            author.DateOfBirth = dateOfBirth;
            return author;
        }

        public IBook CreateNewBook(string title, int releaseYear, IAuthor author, Language language, Genre genre)
        {
            Book book = new Book
            {
                Title = title,
                ReleaseYear = releaseYear,
                Language = language,
                Genre = genre
            };
            if (author is Author bookEntity)
            {
                book.Author = bookEntity;
            }
            else
            {
                throw new InvalidCastException("Author must inherit from BookCatalogue.DAOSQL.BO.Author");
            }
            
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

        public async Task<IEnumerable<IAuthor>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _context.Books.Include(a => a.Author);
        }

        public async Task<IEnumerable<IBook>> GetAllBooksAsync()
        {
            return await _context.Books.Include(a => a.Author).ToListAsync();
        }

        public IAuthor? GetAuthor(Guid id)
        {
            return _context.Authors.FirstOrDefault(a => a.ID == id);
        }

        public async Task<IAuthor?> GetAuthorAsync(Guid id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.ID == id);
        }

        public IBook? GetBook(Guid id)
        {
            return _context.Books.FirstOrDefault(b => b.ID == id);
        }

        public async Task<IBook?> GetBookAsync(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ID == id);
        }


        public void UpdateAuthor(IAuthor author)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.ID == author.ID);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.Surname = author.Surname;
                existingAuthor.DateOfBirth = author.DateOfBirth;
            }
            else
            {
                throw new ArgumentException("Author not found in the database.");
            }
        }

        public void UpdateBook(IBook book)
        {
            var existingBook = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.ID == book.ID);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                if (book is Book bookEntity)
                {
                    var existingAuthor = _context.Authors.FirstOrDefault(a => a.ID == bookEntity.Author.ID);
                    if (existingAuthor != null)
                    {
                        existingBook.Author = existingAuthor;
                    }
                    else
                    {
                        throw new ArgumentException("Author not found in the database.");
                    }
                }
                else
                {
                    throw new InvalidCastException("Book must inherit from BookCatalogue.DAOSQL.BO.Book");
                }
                existingBook.Language = book.Language;
                existingBook.ReleaseYear = book.ReleaseYear;
                existingBook.Genre = book.Genre;
            }
            else
            {
                throw new ArgumentException("Book not found in the database.");
            }
        }

        public bool AuthorExists(Guid id)
        {
            return _context.Authors.Any(a => a.ID == id);
        }

        public bool BookExists(Guid id)
        {
            return _context.Books.Any(b => b.ID == id);
        }
    }
}
