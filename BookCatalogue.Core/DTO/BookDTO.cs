using System.ComponentModel.DataAnnotations;

namespace BookCatalogue.Core.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
        [AuthorIsSet(ErrorMessage = "Author must be selected.")]
        [Display(Name = "Author")]
        public Guid AuthorID { get; set; }
        public AuthorDTO Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
