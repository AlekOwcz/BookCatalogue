using BookCatalogue.Core;
using BookCatalogue.Interfaces;

namespace BookCatalogue.DAOMock.BO
{
    public class Book : IBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public IAuthor Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
