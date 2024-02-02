using BookCatalogue.DAOSQL.BO;
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
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string connectionString = _configuration.GetConnectionString("Sqlite");
            string absoluteConnectionString = Path.Combine(baseDirectory, connectionString);
            optionsBuilder.UseSqlite("Data Source="+Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _configuration.GetConnectionString("Sqlite")));
            //optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
