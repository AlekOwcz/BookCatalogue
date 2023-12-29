using System.ComponentModel.DataAnnotations;

namespace BookCatalogue.UIWeb.Models
{
    public class AuthorDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }
    }
}
