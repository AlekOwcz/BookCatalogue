namespace BookCatalogue.MauiGUI;

public class AuthorsPage : ContentPage
{
    public AuthorsPage(AuthorsCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}