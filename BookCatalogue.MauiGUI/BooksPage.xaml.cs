using BookCatalogue.MauiGUI.ViewModels;

namespace BookCatalogue.MauiGUI;

public partial class BooksPage : ContentPage
{
	public BooksPage(BooksCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}