using System.ComponentModel.DataAnnotations;

namespace BookCatalogue.Core.DTO
{
    public class AuthorDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Author")]
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
