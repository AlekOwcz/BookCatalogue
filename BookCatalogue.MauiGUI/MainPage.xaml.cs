namespace BookCatalogue.MauiGUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBrowseBooksClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///BooksPage");
        }

        private async void OnAddNewBookClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///AuthorsPage");
        }
    }

}
