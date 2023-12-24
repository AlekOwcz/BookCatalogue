using BookCatalogue_Core;
using BookCatalogue_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue_DAOMock
{
    internal class DAOMock : IDAO
    {
        private List<IBook> books;
        private List<IAuthor> authors;

        public DAOMock()
        {
            authors = new List<IAuthor>()
            {
                new BO.Author(){ ID = 1, Name = "Adam", Surname="Mickiewicz", DateOfBirth=new DateTime(1798, 12, 24) },
                new BO.Author(){ ID = 2, Name = "Henryk", Surname="Sienkiewicz", DateOfBirth=new DateTime(1846, 5, 5) },
                new BO.Author(){ ID = 3, Name = "Fiodor", Surname="Dostojewski", DateOfBirth=new DateTime(1812, 11, 11) },
            };
            books = new List<IBook>()
            {
                new BO.Book()
                { 
                    ID = 1, 
                    Title="Pan Tadeusz", 
                    ReleaseYear=1834,
                    Author=authors[0], 
                    Language=Language.POL,
                    Genre=Genre.EPIC
                },
                new BO.Book()
                {
                    ID = 2,
                    Title="Dziady, część III",
                    ReleaseYear=1832,
                    Author=authors[0],
                    Language=Language.POL,
                    Genre=Genre.DRAMA
                },
                new BO.Book()
                {
                    ID = 3,
                    Title="Zbrodnia i kara",
                    ReleaseYear=1867,
                    Author=authors[2],
                    Language=Language.RUS,
                    Genre=Genre.PSYCHOLOGICAL
                }
            };
        }

        public IAuthor CreateNewAuthor()
        {
            return new BO.Author();
        }

        public IBook CreateNewBook()
        {
            return new BO.Book();
        }

        public IEnumerable<IAuthor> GetAllAuthors()
        {
            return authors;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return books;
        }
    }
}
