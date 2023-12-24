using BookCatalogue.Core;
using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BookCatalogue.DAOSQL.BO
{
    [Index(nameof(Title), nameof(Author), nameof(Language), IsUnique = true)]
    public class Book : IBook
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public IAuthor Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
