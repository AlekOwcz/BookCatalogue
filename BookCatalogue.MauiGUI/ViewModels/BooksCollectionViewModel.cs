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

namespace BookCatalogue.MauiGUI.ViewModels
{
    
    public partial class BooksCollectionViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<BookViewModel> books;

        private readonly BLC.BLC _blc;

        [ObservableProperty]
        private BookViewModel _bookEdit;

        [ObservableProperty]
        private bool _isEditing;
        private AuthorViewModel _chosenAuthor;
        public AuthorViewModel ChosenAuthor
        {
            get { return _chosenAuthor; }
            set
            {
                if (_chosenAuthor != value)
                    _chosenAuthor = value;
                OnPropertyChanged(nameof(ChosenAuthor));
            }
        }
        public BooksCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;
            books = new ObservableCollection<BookViewModel>();
            foreach (var book in blc.GetAllBooks())
            {
                books.Add(new BookViewModel(book));
            }
            IsEditing= false;
            _bookEdit = null;

            CancelCommand = new Command(
                            execute: () =>
                            {
                                _bookEdit.PropertyChanged -= OnPersonEditPropertyChanged;
                                _bookEdit = null;
                                IsEditing = false;
                                RefreshCanExecute();
                            },
                            canExecute: () => IsEditing

                            );
   
        }
       


        [RelayCommand(CanExecute = nameof(CanCreateNewBook))]
        private void CreateNewBook()
        {
            _bookEdit = new BookViewModel();
            _bookEdit.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private void OnPersonEditPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SaveBookCommand.NotifyCanExecuteChanged();
        }

        private void DeleteBook() { }
        private bool CanCreateNewBook() 
        { 
            return !IsEditing; 
        }

        private void RefreshCanExecute()
        {
            CreateNewBookCommand.NotifyCanExecuteChanged();
            SaveBookCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }
        [RelayCommand(CanExecute = nameof(CanEditBookBeSaved))]
        private void SaveBook()
        {
            Books.Add(_bookEdit);
            _blc.AddBook((IBook)_bookEdit);
            _blc.SaveChangesAsync();
            _bookEdit.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            _bookEdit = null;
            RefreshCanExecute();
        }

        private bool CanEditBookBeSaved()
        {   //TODO: dodaj weryfikację
            return this._bookEdit != null && this._bookEdit.Title != null;
                
        }
        //On cancel      BookEdit.PropertyChanged -= OnBookEditPropertyChanged;

        public ICommand CancelCommand {  get; set; }
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

   
}
