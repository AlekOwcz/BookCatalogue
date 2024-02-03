using BookCatalogue.MauiGUI.ViewModels;
using Microsoft.Extensions.Logging.Abstractions;

namespace BookCatalogue.MauiGUI;

public partial class BooksPage : ContentPage
{
    private readonly BLC.BLC _context;

    public BooksPage(BooksCollectionViewModel viewModel, BLC.BLC context)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _context = context;
       // LoadAuthors();
	}

    //private void LoadAuthors()
    //{
    //    var authors = _context.GetAllAuthors();

    //    foreach (var author in authors)
    //    {
    //        AuthorEntry.Items.Add(author.FullName);
    //    }
    //}

    private void EnableFields(object sender, EventArgs e)
    {
        
        GenrePicker.IsEnabled = true;
        LanguagePicker.IsEnabled = true;
    }
    private void DisableFields(object sender, EventArgs e)
    {
        
        GenrePicker.IsEnabled = false;
        LanguagePicker.IsEnabled = false;
    }
}