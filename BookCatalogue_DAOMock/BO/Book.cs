using BookCatalogue_Core;
using BookCatalogue_Interfaces;

namespace BookCatalogue_DAOMock.BO
{
    public class Book : IBook
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public IAuthor Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
