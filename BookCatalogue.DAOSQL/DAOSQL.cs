using BookCatalogue.Core;
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
            Author newAuthor = new Author()
            {
                Id = Guid.NewGuid(),
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
        }

        public void AddBook(IBook book)
        {
            Book newBook = new Book()
            {
                Id = Guid.NewGuid(),
                Title = book.Title,
                ReleaseYear = book.ReleaseYear,
                Author = (Author)book.Author,
                Language = book.Language,
                Genre = book.Genre
            };
            _context.Books.Add(newBook);
            _context.SaveChanges();
        }

        public void DeleteAuthor(IAuthor author)
        {
            var authorToDelete = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
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
            var bookToDelete = _context.Books.FirstOrDefault(b => b.Id == book.Id);
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
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public async Task<IAuthor?> GetAuthorAsync(Guid id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public IBook? GetBook(Guid id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public async Task<IBook?> GetBookAsync(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public void UpdateAuthor(IAuthor author)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
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
            var existingBook = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                var existingAuthor = _context.Authors.FirstOrDefault(a => a.Id == book.Author.Id);
                if (existingAuthor != null)
                {
                    existingBook.Author = existingAuthor;
                }
                else
                {
                    throw new ArgumentException("Author not found in the database.");
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
            return _context.Authors.Any(a => a.Id == id);
        }

        public bool BookExists(Guid id)
        {
            return _context.Books.Any(b => b.Id == id);
        }
    }
}
