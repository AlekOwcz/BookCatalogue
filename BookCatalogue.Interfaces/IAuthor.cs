using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Interfaces
{
    public interface IAuthor
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        DateTime DateOfBirth { get; set; }
        public string FullName { get; }
    }
}
