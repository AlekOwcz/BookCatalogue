using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IBook> GetAllBooks();
        IEnumerable<IAuthor> GetAllAuthors();

        IAuthor CreateNewAuthor();
        IBook CreateNewBook();
    }
}
