using BookCatalogue.Core;
using BookCatalogue.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DefaultValue(0)]
        private Language language = Language.Polish;

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
        public BookViewModel()
        {
            Id= Guid.NewGuid();
        }


        public IReadOnlyList<string> AllLanguages { get; } = Enum.GetNames(typeof(Language));
    }
}

