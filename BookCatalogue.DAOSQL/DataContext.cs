using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.DAOSQL
{
    public class DataContext: DbContext
    {
        IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        public virtual DbSet<BO.Author> Authors { get; set; }
        public virtual DbSet<BO.Book> Books { get; set; }

    }
}
