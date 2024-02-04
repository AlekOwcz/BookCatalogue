using BookCatalogue.MauiGUI.ViewModels;

namespace BookCatalogue.MauiGUI;

public partial class AuthorsPage : ContentPage
{
    private AuthorsCollectionViewModel _viewModel;
	public AuthorsPage(AuthorsCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

	private void EnableFields(object sender, EventArgs e)
	{
		AuthorEditor.IsVisible = true;
	}
    private void DisableFields(object sender, EventArgs e)
    {
        if (!_viewModel.IsCreating && !_viewModel.IsEditing)
        {
            AuthorEditor.IsVisible = false;
        }

    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        EnableFields(sender, e);
        _viewModel.StartEditing(e.SelectedItemIndex);
    }


}