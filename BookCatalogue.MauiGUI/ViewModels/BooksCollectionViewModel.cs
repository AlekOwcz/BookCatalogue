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
        public BooksCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;
            books = new ObservableCollection<BookViewModel>();
            foreach (var book in blc.GetAllBooks())
            {
                books.Add(new BookViewModel(book));
            }
            IsEditing= false;
            bookEdit = null;

            CancelCommand = new Command(
                            execute: () =>
                            {
                                BookEdit.PropertyChanged -= OnBookEditPropertyChanged;
                                BookEdit = null;
                                IsEditing = false;
                                RefreshCanExecute();
                            },
                            canExecute: () => IsEditing

                            );
   
        }

        [ObservableProperty]
        private BookViewModel bookEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute = nameof(CanCreateNewBook))]
        private void CreateNewBook()
        {
            BookEdit = new BookViewModel();
            BookEdit.PropertyChanged += OnBookEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private void OnBookEditPropertyChanged(object? sender, PropertyChangedEventArgs e)
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
            Books.Add(BookEdit);
            BookEdit.PropertyChanged -= OnBookEditPropertyChanged;
            IsEditing = false;
            BookEdit = null;
            RefreshCanExecute();
        }

        private bool CanEditBookBeSaved()
        {   //TODO: dodaj weryfikację
            return BookEdit != null && BookEdit.Author != null ;
                
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
