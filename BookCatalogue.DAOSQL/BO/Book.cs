using BookCatalogue.Core;
using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BookCatalogue.DAOSQL.BO
{
    [Index(nameof(Title), IsUnique = true)]
    public class Book : IBook
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        //public IAuthor Author { get; set; }
        public virtual Author Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
        [NotMapped]
        IAuthor IBook.Author 
        {
            get 
            {
                return Author;
            }
            set 
            { 
                if(!(value is Author))
                {
                    throw new InvalidCastException("Author must inherit from BookCatalogue.DAOSQL.BO.Author");
                }
                Author = (Author)value;
            }
        }
    }
}
