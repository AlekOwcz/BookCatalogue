using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Interfaces
{
    public interface IAuthor
    {
        Guid ID { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        DateTime DateOfBirth { get; set; }
    }
}
