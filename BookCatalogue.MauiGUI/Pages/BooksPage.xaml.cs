using BookCatalogue.Interfaces;
using BookCatalogue.MauiGUI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.ObjectModel;

namespace BookCatalogue.MauiGUI;

public partial class BooksPage : ContentPage
{
    private readonly BLC.BLC _context;

    private BooksCollectionViewModel _viewModel;

    public BooksPage(BooksCollectionViewModel viewModel, BLC.BLC context)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _context = context;
        _viewModel = viewModel;
    }


    private void EnableFields(object sender, EventArgs e)
    {
        BookEditor.IsVisible = true;
    }
    private void DisableFields(object sender, EventArgs e)
    {
        if (!_viewModel.IsCreating && !_viewModel.IsEditing)
        {
            BookEditor.IsVisible = false;
        }
    }
    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        EnableFields(sender, e);
        _viewModel.StartEditing(e.SelectedItemIndex);
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue.ToLowerInvariant();
        _viewModel.ApplySearchFiltering(searchText);
    }

}