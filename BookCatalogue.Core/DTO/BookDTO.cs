namespace BookCatalogue.Core.DTO
{
    public class BookDTO
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public AuthorDTO Author { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
    }
}
