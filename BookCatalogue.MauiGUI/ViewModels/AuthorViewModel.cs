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
    public partial class AuthorViewModel : ObservableValidator, IAuthor
    {
        [ObservableProperty]
        public Guid id;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string surname;
        [ObservableProperty]
        public DateTime dateOfBirth;
        [ObservableProperty]
        public string fullName;

        public AuthorViewModel(IAuthor author)
        {
            Id = author.Id;
            Name = author.Name;
            Surname = author.Surname;
            DateOfBirth = author.DateOfBirth;
            FullName = author.FullName;
        }
        public AuthorViewModel()
        {

        }
    }
}
