using BookCatalogue.Core;
using BookCatalogue.Core.DTO;
using BookCatalogue.DAOMock.BO;
using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.DAOMock
{
    public class DAOMock : IDAO
    {
        private List<IBook> _books;
        private List<IAuthor> _authors;

        public DAOMock()
        {
            _authors =
            [
                new Author(){ Id = Guid.NewGuid(), Name = "Adam", Surname="Mickiewicz", DateOfBirth=new DateTime(1798, 12, 24) },
                new Author(){ Id = Guid.NewGuid(), Name = "Henryk", Surname="Sienkiewicz", DateOfBirth=new DateTime(1846, 5, 5) },
                new Author(){ Id = Guid.NewGuid(), Name = "Fiodor", Surname="Dostojewski", DateOfBirth=new DateTime(1812, 11, 11) },
            ];
            _books =
            [
                new Book()
                { 
                    Id = Guid.NewGuid(), 
                    Title="Pan Tadeusz", 
                    ReleaseYear=1834,
                    Author=_authors[0], 
                    Language=Language.Polish,
                    Genre=Genre.Epic
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title="Dziady, część III",
                    ReleaseYear=1832,
                    Author=_authors[0],
                    Language=Language.Polish,
                    Genre=Genre.Drama
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title="Zbrodnia i kara",
                    ReleaseYear=1867,
                    Author=_authors[2],
                    Language=Language.Russian,
                    Genre=Genre.Psychological
                }
            ];
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }

        public void AddAuthor(IAuthor author)
        {
            if (author is Author)
            {
                author.Id = Guid.NewGuid();
                _authors.Add(author);
            }
            else
            {
                throw new ArgumentException("The author must be of type BookCatalogue.DAOMock.BO.Author.");
            }
        }

        public void AddBook(IBook book)
        {
            if (book is Book)
            {
                book.Id = Guid.NewGuid();
                _books.Add(book);
            }
            else
            {
                throw new ArgumentException("The book must be of type BookCatalogue.DAOMock.BO.Book.");
            }
        }


        public IAuthor ConvertToIAuthor(AuthorDTO authorDTO)
        {
            IAuthor author = new Author()
            {
                Id = authorDTO.Id,
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
                Id = bookDTO.Id,
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
            Author author = new Author
            {
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth
            };
            return author;
        }

        public IBook CreateNewBook(string title, int releaseYear, IAuthor author, Language language, Genre genre)
        {
            BO.Book book = new BO.Book();
            book.Title = title;
            book.ReleaseYear = releaseYear;
            book.Author = author;
            book.Language = language;
            book.Genre = genre;
            return book;
        }


        public void DeleteAuthor(IAuthor author)
        {
            _books.RemoveAll(b => b.Author.Id == author.Id);
            _authors.Remove(author);
        }

        public void DeleteBook(IBook book)
        {
            _books.Remove(book);
        }


        public IEnumerable<IAuthor> GetAllAuthors()
        {
            return _authors.AsEnumerable();
        }


        public Task<IEnumerable<IAuthor>> GetAllAuthorsAsync()
        {
            return Task.FromResult(GetAllAuthors());
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _books.AsEnumerable();
        }

        public Task<IEnumerable<IBook>> GetAllBooksAsync()
        {
            return Task.FromResult(GetAllBooks());
        }


        public IAuthor? GetAuthor(Guid id)
        {
            return _authors.FirstOrDefault(a => a.Id == id);
        }

        public async Task<IAuthor?> GetAuthorAsync(Guid id)
        {
            return await Task.FromResult(GetAuthor(id));
        }

        public IBook? GetBook(Guid id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public async Task<IBook?> GetBookAsync(Guid id)
        {
            return await Task.FromResult(GetBook(id));
        }


        public void UpdateAuthor(IAuthor author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);
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
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Language = book.Language;
                existingBook.ReleaseYear = book.ReleaseYear;
                existingBook.Genre = book.Genre;
            }
        }

        public bool AuthorExists(Guid id)
        {
            return _authors.Any(a => a.Id == id);
        }

        public bool BookExists(Guid id)
        {
            return _books.Any(b => b.Id == id);
        }
    }
}
