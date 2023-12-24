using BookCatalogue_Core;

namespace BookCatalogue_Interfaces
{
    public interface IBook
    {
        int ID { get; set; }
        string Title { get; set; }
        public int ReleaseYear { get; set; }
        IAuthor Author { get; set; }
        Language Language { get; set; }
        Genre Genre { get; set; }
    }
}
