using BookCatalogue.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BookCatalogue.MauiGUI.ViewModels
{
    public partial class AuthorsCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<AuthorViewModel> authorsCollection;

        private AuthorViewModel? _selectedAuthor;
        public AuthorViewModel? SelectedAuthor
        {
            get => _selectedAuthor;
            set => SetProperty(ref _selectedAuthor, value);
        }
        private readonly BLC.BLC _blc;
        public AuthorsCollectionViewModel(BLC.BLC blc)
        {
            _blc = blc;
            AuthorsCollection = new ObservableCollection<AuthorViewModel>();
            foreach (var author in _blc.GetAllAuthors())
            {
                AuthorsCollection.Add(new AuthorViewModel(author));
            }
        }

        //[ObservableProperty]
        //private AuthorViewModel authorEdit;

        [ObservableProperty]
        private bool isEditing, isCreating;

        [RelayCommand(CanExecute = nameof(CanCreateNewAuthor))]
        private void CreateNewAuthor()
        {
            SelectedAuthor = new AuthorViewModel();
            SelectedAuthor.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = false;
            IsCreating = true;
            RefreshCanExecute();
        }

        private void OnPersonEditPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SaveAuthorCommand.NotifyCanExecuteChanged();
            RemoveAuthorCommand.NotifyCanExecuteChanged();
        }

        private bool CanCreateNewAuthor()
        {
            return !IsEditing && !IsCreating;
        }

        private void RefreshCanExecute()
        {
            CreateNewAuthorCommand.NotifyCanExecuteChanged();
            SaveAuthorCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
            RemoveAuthorCommand.NotifyCanExecuteChanged();
        }
        [RelayCommand(CanExecute = nameof(CanEditAuthorBeSaved))]
        private void SaveAuthor()
        {
            if (Validate())
            {
                if (IsCreating)
                {
                    AuthorsCollection.Add(SelectedAuthor);

                    IAuthor authorToUpdate = SelectedAuthor;
                    _blc.AddAuthor(authorToUpdate);


                }
                if (IsEditing)
                {
                    IAuthor authorToUpdate = SelectedAuthor;
                    AuthorsCollection.RemoveAt(EditedAuthorID);
                    AuthorsCollection.Insert(EditedAuthorID, SelectedAuthor);
                    _blc.UpdateAuthor(authorToUpdate);
                }
                _blc.SaveChangesAsync();
                SelectedAuthor.PropertyChanged -= OnPersonEditPropertyChanged;
                IsEditing = false;
                IsCreating = false;
                SelectedAuthor = null;
                RefreshCanExecute();
            }

        }
        private bool Validate()
        {
            if (SelectedAuthor != null)
            {
                if (SelectedAuthor.Name == null || SelectedAuthor.Name.Length <= 0)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Author Error", "Name should not be empty", "Ok");
                    return false;
                }
                    
                if (SelectedAuthor.Surname == null || SelectedAuthor.Surname.Length <= 0)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Author Error", "Surname should not be empty", "Ok");
                    return false;
                }
                if (SelectedAuthor.DateOfBirth > DateTime.Now)
                {
                    _ = Application.Current?.MainPage?.DisplayAlert("Author Error", "Author should not be from the future", "Ok");
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
            SelectedAuthor.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            IsCreating = false;
            SelectedAuthor = null;
            RefreshCanExecute();
        }
        [ObservableProperty]
        private bool _isValidName,_isValidSurname,_isValidDate;

        private bool CanEditAuthorBeSaved()
        {   
            
            return IsCreating || IsEditing;
        }
        [RelayCommand(CanExecute =nameof(IsEditing))]
        public void RemoveAuthor()
        {
            AuthorsCollection.RemoveAt(EditedAuthorID);
            if (SelectedAuthor != null && _blc.AuthorExists(SelectedAuthor.id))
            {
                _blc.RemoveAuthor(SelectedAuthor);

            }
            SelectedAuthor.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = false;
            IsCreating = false;
            SelectedAuthor = null;
            RefreshCanExecute();
        }
        private int EditedAuthorID;
        [RelayCommand]
        public void StartEditing(int SelectedId)
        {
            if (!IsCreating)
            {
                EditedAuthorID = SelectedId;
                SelectedAuthor = new AuthorViewModel(AuthorsCollection.ElementAt(SelectedId));
                SelectedAuthor.PropertyChanged += OnPersonEditPropertyChanged;
                IsEditing = true;
                IsCreating = false;
                RefreshCanExecute();
            }
        }

        public class NullConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return value != null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    }
}

