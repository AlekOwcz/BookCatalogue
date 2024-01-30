using BookCatalogue.Core;
using BookCatalogue.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.MauiGUI.ViewModels
{
    public partial class BookViewModel:ObservableObject, IBook
    {
        [ObservableProperty]
        private Guid id;

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private int releaseYear;

        [ObservableProperty]
        private IAuthor? author;

        [ObservableProperty]
        private Language language;

        [ObservableProperty]
        private Genre genre;

        public BookViewModel(IBook book)
        {
            Id= book.Id;
            Title= book.Title;
            ReleaseYear= book.ReleaseYear;
            Author= book.Author;
            Language= book.Language;
            Genre= book.Genre;
        }

    }
}

