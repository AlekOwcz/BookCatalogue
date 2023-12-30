using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.DAOSQL.BO
{
    [Index(nameof(Name), nameof(Surname), IsUnique = true)]
    public class Author : IAuthor
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
