using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookCatalogue.MauiGUI.ViewModels
{
    public partial class AuthorsCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<AuthorViewModel> authors;
        private readonly BLC.BLC _blc;
        public AuthorsCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;
            authors = new ObservableCollection<AuthorViewModel>();
            foreach (var book in _blc.GetAllAuthors())
            {
                authors.Add(new AuthorViewModel(book));
            }
        }
        [ObservableProperty]
        private AuthorViewModel authorEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute = nameof(CanCreateNewAuthor))]
        private void CreateNewAuthor()
        {
            AuthorEdit = new AuthorViewModel();
            AuthorEdit.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private void OnPersonEditPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SaveAuthorCommand.NotifyCanExecuteChanged();
        }

        private void DeleteBook() { }
        private bool CanCreateNewAuthor()
        {
            return !IsEditing;
        }

        private void RefreshCanExecute()
        {
            CreateNewAuthorCommand.NotifyCanExecuteChanged();
            SaveAuthorCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
        }
        [RelayCommand(CanExecute = nameof(CanEditAuthorBeSaved))]
        private void SaveAuthor()
        {
            Authors.Add(AuthorEdit);
            AuthorEdit.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            AuthorEdit = null;
            RefreshCanExecute();
        }
        [RelayCommand(CanExecute = nameof(IsEditing))]
        private void Cancel()
        {
            AuthorEdit.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            AuthorEdit = null;
            RefreshCanExecute();
        }


        private bool CanEditAuthorBeSaved()
        {   //TODO: dodaj weryfikację
            return AuthorEdit != null && AuthorEdit.Name != null && AuthorEdit.Surname != null;

        }
        

    }
}

