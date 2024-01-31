using BookCatalogue.MauiGUI.ViewModels;

namespace BookCatalogue.MauiGUI;

public partial class AuthorsPage : ContentPage
{
	public AuthorsPage(AuthorsCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}