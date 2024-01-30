using BookCatalogue.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
//using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

    }
    
}
