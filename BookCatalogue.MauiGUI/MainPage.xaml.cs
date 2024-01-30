namespace BookCatalogue.MauiGUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private async void OnBrowseBooksClicked(object sender, EventArgs e)
        {
            // Tu dodaj kod obsługi przycisku "Browse Books"
            //await Navigation.PushAsync(new BooksPage());
        }

        private async void OnAddNewBookClicked(object sender, EventArgs e)
        {
            // Tu dodaj kod obsługi przycisku "Add New Book"
            //await Navigation.PushAsync(new AddBookPage());
        }
    }

}
