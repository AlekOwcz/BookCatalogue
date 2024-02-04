using BookCatalogue.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using BookCatalogue.Core;
using System.Runtime.InteropServices;
using BookCatalogue.DAOMock.BO;
using BookCatalogue.BLC;
using Microsoft.Extensions.Options;

namespace BookCatalogue.MauiGUI.ViewModels
{
    
    public partial class BooksCollectionViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<BookViewModel> books;
        [ObservableProperty]
        private ObservableCollection<BookViewModel> filteredBooks;
        private BookViewModel? _selectedBook;
        public BookViewModel? SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
        }
        [ObservableProperty]
        private ObservableCollection<AuthorViewModel> authorsCollection;
      
        private readonly BLC.BLC _blc;


        [ObservableProperty]
        private bool _isEditing, _isCreating;
        private IAuthor? _chosenAuthor;
        public IAuthor? ChosenAuthor
        {
            get => _chosenAuthor;
            set => SetProperty(ref _chosenAuthor, value);
        }

        public BooksCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;
            Books = new ObservableCollection<BookViewModel>();
            foreach (var book in blc.GetAllBooks())
            {
                Books.Add(new BookViewModel(book));
            }
            FilteredBooks = new ObservableCollection<BookViewModel>(Books);
            AuthorsCollection = new ObservableCollection<AuthorViewModel>();
            foreach (var author in blc.GetAllAuthors())
            {
                AuthorsCollection.Add(new AuthorViewModel(author));
            }
            IsEditing = false;
            IsCreating=false;
            SelectedBook = null;

   
        }


        [RelayCommand(CanExecute = nameof(CanCreateNewBook))]
        private void CreateNewBook()
        {
            foreach (var author in _blc.GetAllAuthors())
            {
                AuthorsCollection.Add(new AuthorViewModel(author));
            }
            SelectedBook = new BookViewModel();
            SelectedBook.Author = AuthorsCollection.First();
            SelectedBook.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = false;
            IsCreating = true;
            RefreshCanExecute();
        }

        private void OnPersonEditPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SaveBookCommand.NotifyCanExecuteChanged();
            RemoveBookCommand.NotifyCanExecuteChanged();
        }
        private bool CanCreateNewBook()
        {
            return !IsEditing && !IsCreating;
        }

        private void RefreshCanExecute()
        {
            CreateNewBookCommand.NotifyCanExecuteChanged();
            SaveBookCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
            RemoveBookCommand.NotifyCanExecuteChanged();
        }
        [RelayCommand(CanExecute = nameof(CanEditBookBeSaved))]
        private void SaveBook()
        {
           
            if (Validate())
            {
                IAuthor? author = _blc.GetAuthor(ChosenAuthor.Id);
                if (author != null && SelectedBook.ReleaseYear <= author.DateOfBirth.Year)
                {
                    _ = Application.Current.MainPage.DisplayAlert("Author Error", "Book cannot be released before author was born", "Ok");
                    return;
                }
                IBook bookToUpdate = SelectedBook;
                bookToUpdate.Author = author;
                if (IsEditing)
                {
                    FilteredBooks.RemoveAt(EditedBookId);
                    FilteredBooks.Insert(EditedBookId, SelectedBook);
                    _blc.RemoveBook(bookToUpdate);
                    _blc.AddBook(bookToUpdate);
                }
                if(IsCreating)
                {

                    FilteredBooks.Add(SelectedBook);
                    _blc.AddBook(bookToUpdate);
                }
                _blc.SaveChangesAsync();
                RefreshBooks();
                SelectedBook.PropertyChanged -= OnPersonEditPropertyChanged;
                IsEditing = false;
                IsCreating = false;
                SelectedBook = null;
            }
            RefreshCanExecute();
        }
        private bool Validate()
        {
            if (SelectedBook != null)
            {
                if (SelectedBook.Title == null || SelectedBook.Title.Length <= 0)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Book Error", "Title should not be empty", "Ok");
                    return false;
                }
                if (ChosenAuthor == null )
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Book Error", "Select an author of book or create the author first in Authors tab", "Ok");
                    return false;
                }
                //if(ChosenAuthor == null && IsCreating) {
                  //  _ = Application.Current?.MainPage?.DisplayAlert("Book Error", "Select an author of book or create the author first in Authors tab", "Ok");
                    //return false;
               // }

                if (SelectedBook.Language == null)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Book Error", "Select the language of the book", "Ok");
                    return false;
                }
                if (SelectedBook.Genre == null)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Book Error", "Select the genre of the book", "Ok");
                    return false;
                }
                if (SelectedBook.ReleaseYear > DateTime.Now.Year)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Book Error", "Book should not be from the future", "Ok");
                    return false;
                }
                return true;

            }
            else
            {
                _ = Application.Current?.MainPage?.DisplayAlert("Author Error", "Selected Author is empty", "Ok");
                return false;
            }
            return true;
        }
        bool CanCancel()
        {
            return IsCreating || IsEditing;
        }

        [RelayCommand(CanExecute = nameof(CanCancel))]
        private void Cancel()
        {
            SelectedBook.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            IsCreating = false;
            SelectedBook = null;
            RefreshCanExecute();
        }
        [ObservableProperty]
        private bool _isValidName;

        private bool CanEditBookBeSaved()
        {   

            return IsCreating || IsEditing;
        }
        [RelayCommand(CanExecute = nameof(IsEditing))]
        public void RemoveBook()
        {
            FilteredBooks.RemoveAt(EditedBookId);
            if (SelectedBook != null && _blc.BookExists(SelectedBook.Id))
            {
                _blc.RemoveBook(SelectedBook);
            }
            RefreshBooks();
            SelectedBook.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = false;
            IsCreating = false;
            SelectedBook = null;
            RefreshCanExecute();
        }

        private int EditedBookId;
        [RelayCommand]
        public void StartEditing(int SelectedId)
        {
            if (!IsCreating && !IsEditing)
            {
                foreach (var author in _blc.GetAllAuthors())
                {
                    AuthorsCollection.Add(new AuthorViewModel(author));
                }
                EditedBookId = SelectedId;
                SelectedBook = new BookViewModel(FilteredBooks.ElementAt(SelectedId));
                ChosenAuthor = SelectedBook.Author;
                SelectedBook.PropertyChanged += OnPersonEditPropertyChanged;
                IsEditing = true;
                IsCreating = false;
                RefreshCanExecute();
            }
        }
        void RefreshBooks()
        {
            Books.Clear();
            foreach (var book in _blc.GetAllBooks())
            {
                Books.Add(new BookViewModel(book));
            }
        }
        public void ApplySearchFiltering(string text)
        {
            
            FilteredBooks.Clear();
            var searchText = text.Trim();
            var filteredStorages = Books.Where(book =>
                book.Title.ToLowerInvariant().Contains(searchText) ||
                book.Author.FullName.ToLowerInvariant().Contains(searchText) ||
                book.Genre.ToString().ToLowerInvariant().Contains(searchText) ||
                book.Language.ToString().ToLowerInvariant().Contains(searchText) ||
                book.ReleaseYear.ToString().ToLowerInvariant().Contains(searchText)
            );
            foreach (var item in filteredStorages)
            {
                FilteredBooks.Add(item);
            }
        }
    }


    public class LanguageEnumToIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Language language = (Language)value;
            int result = (int)language;
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val != -1)
                return (Language)value;
            else
                return 0;

        }

    }
    public class GenreEnumToIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Genre language = (Genre)value;
            int result = (int)language;
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val != -1)
                return (Genre)value;
            else
                return 0;

        }

    }


   
}
