﻿using BookCatalogue.Interfaces;
using System.ComponentModel.DataAnnotations;
using BookCatalogue.Core;

namespace BookCatalogue.UIWeb.DTO
{
    public class BookDTO: IBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
        [AuthorIsSet(ErrorMessage = "Author must be selected.")]
        [Display(Name = "Author")]
        public Guid AuthorID { get; set; }
        public IAuthor Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
