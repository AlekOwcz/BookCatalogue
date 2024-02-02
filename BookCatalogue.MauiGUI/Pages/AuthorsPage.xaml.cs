using BookCatalogue.MauiGUI.ViewModels;

namespace BookCatalogue.MauiGUI;

public partial class AuthorsPage : ContentPage
{
	public AuthorsPage(AuthorsCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

	private void EnableFields(object sender, EventArgs e)
	{
		NameEntry.IsEnabled = true;
		SurnameEntry.IsEnabled = true;
		DOBEntry.IsEnabled = true;
	}
    private void DisableFields(object sender, EventArgs e)
    {
        NameEntry.IsEnabled = false;
        SurnameEntry.IsEnabled = false;
        DOBEntry.IsEnabled = false;
    }
}