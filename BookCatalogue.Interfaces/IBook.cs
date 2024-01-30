using BookCatalogue.Core;

namespace BookCatalogue.Interfaces
{
    public interface IBook
    {
        Guid Id { get; set; }
        string Title { get; set; }
        int ReleaseYear { get; set; }
        IAuthor Author { get; set; }
        Language Language { get; set; }
        Genre Genre { get; set; }
    }
}
