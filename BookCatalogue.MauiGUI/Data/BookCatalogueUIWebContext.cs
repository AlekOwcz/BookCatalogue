using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookCatalogue.Core.DTO;

namespace BookCatalogue.UIWeb.Data
{
    public class BookCatalogueUIWebContext : DbContext
    {
        public BookCatalogueUIWebContext (DbContextOptions<BookCatalogueUIWebContext> options)
            : base(options)
        {
        }

        public DbSet<BookCatalogue.Core.DTO.BookDTO> BookDTO { get; set; } = default!;
    }
}
