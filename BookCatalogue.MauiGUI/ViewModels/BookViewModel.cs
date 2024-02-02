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
    public partial class BookViewModel:ObservableValidator, IBook
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Id must be given")]
        private Guid id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(1, ErrorMessage = "Title must be not empty")]
        private string title;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Release year must be positive and not in the future")]
        [Range(0, 2024)]
        private int releaseYear;

        [ObservableProperty]
       // [MinLength(2, ErrorMessage = "Author must have at leat 2 characters")]
        private IAuthor author;

        [ObservableProperty]
        [DefaultValue(0)]
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
        public BookViewModel()
        {
            Id= Guid.NewGuid();
        }


        public IReadOnlyList<string> AllLanguages { get; } = Enum.GetNames(typeof(Language));
        public IReadOnlyList<string> AllGenres { get; } = Enum.GetNames(typeof(Genre));
    }
}

