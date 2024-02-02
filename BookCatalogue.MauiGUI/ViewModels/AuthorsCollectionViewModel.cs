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

        private AuthorViewModel _selectedAuthor;
        public AuthorViewModel SelectedAuthor
        {
            get => _selectedAuthor;
            set => SetProperty(ref _selectedAuthor, value);
        }
        public IReadOnlyList<string> AllAuthors()
        {
            List<string> strings = new List<string>();
            foreach (AuthorViewModel author in AuthorsCollection)
            {
                strings.Add(author.fullName);
            }
            return strings;
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
            _selectedAuthor=AuthorsCollection.First();
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
            isCreating = true;
            RefreshCanExecute();
        }

        private void OnPersonEditPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SaveAuthorCommand.NotifyCanExecuteChanged();
        }

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
            if (IsCreating)
            {
                AuthorsCollection.Add(SelectedAuthor);

                IAuthor authorToUpdate = SelectedAuthor;
                _blc.AddAuthor(authorToUpdate);


            }
            if (IsEditing)
            {
                IAuthor authorToUpdate = SelectedAuthor;
                _blc.UpdateAuthor(authorToUpdate);
            }
            _blc.SaveChangesAsync();
            SelectedAuthor.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            isCreating = false;
            SelectedAuthor = null;
            RefreshCanExecute();
        }
        [RelayCommand(CanExecute = nameof(IsEditing))]
        private void Cancel()
        {
            SelectedAuthor.PropertyChanged -= OnPersonEditPropertyChanged;
            IsEditing = false;
            isCreating = false;
            SelectedAuthor = null;
            RefreshCanExecute();
        }


        private bool CanEditAuthorBeSaved()
        {   //TODO: dodaj weryfikację
            return SelectedAuthor != null && SelectedAuthor.Name != null && SelectedAuthor.Surname != null;

        }

        [RelayCommand]
        public void Remove()
        {
            AuthorsCollection.Remove(SelectedAuthor);
            if (SelectedAuthor != null)
            {
                this._blc.RemoveAuthor(_selectedAuthor);

            }
        }

    }
}

