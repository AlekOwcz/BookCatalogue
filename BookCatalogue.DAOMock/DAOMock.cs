using BookCatalogue.Core;
using BookCatalogue.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.DAOMock
{
    internal class DAOMock : IDAO
    {
        private List<IBook> _books;
        private List<IAuthor> _authors;

        public DAOMock()
        {
            _authors = new List<IAuthor>()
            {
                new BO.Author(){ ID = Guid.NewGuid(), Name = "Adam", Surname="Mickiewicz", DateOfBirth=new DateTime(1798, 12, 24) },
                new BO.Author(){ ID = Guid.NewGuid(), Name = "Henryk", Surname="Sienkiewicz", DateOfBirth=new DateTime(1846, 5, 5) },
                new BO.Author(){ ID = Guid.NewGuid(), Name = "Fiodor", Surname="Dostojewski", DateOfBirth=new DateTime(1812, 11, 11) },
            };
            _books = new List<IBook>()
            {
                new BO.Book()
                { 
                    ID = Guid.NewGuid(), 
                    Title="Pan Tadeusz", 
                    ReleaseYear=1834,
                    Author=_authors[0], 
                    Language=Language.Polish,
                    Genre=Genre.Epic
                },
                new BO.Book()
                {
                    ID = Guid.NewGuid(),
                    Title="Dziady, część III",
                    ReleaseYear=1832,
                    Author=_authors[0],
                    Language=Language.Polish,
                    Genre=Genre.Drama
                },
                new BO.Book()
                {
                    ID = Guid.NewGuid(),
                    Title="Zbrodnia i kara",
                    ReleaseYear=1867,
                    Author=_authors[2],
                    Language=Language.Russian,
                    Genre=Genre.Psychological
                }
            };
        }

        public void AddAuthor(IAuthor author)
        {
            author.ID = Guid.NewGuid();
            _authors.Add(author);
        }

        public void AddBook(IBook book)
        {
            book.ID = Guid.NewGuid();
            _books.Add(book);
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
            book.Author = author;
            book.Language = language;
            book.Genre = genre;
            return book;
        }

        public void DeleteAuthor(IAuthor author)
        {
            _authors.Remove(author);
        }

        public void DeleteBook(IBook book)
        {
            _books.Remove(book);
        }

        public IEnumerable<IAuthor> GetAllAuthors()
        {
            return _authors;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _books;
        }

        public void UpdateAuthor(IAuthor author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.ID == author.ID);
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
            var existingBook = _books.FirstOrDefault(b => b.ID == book.ID);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Language = book.Language;
                existingBook.ReleaseYear = book.ReleaseYear;
                existingBook.Genre = book.Genre;
            }
        }
    }
}
