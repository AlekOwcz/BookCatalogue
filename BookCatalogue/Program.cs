using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BookCatalogue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"];

            BLC.BLC blc = BLC.BLC.GetInstance(libraryName);

            foreach (var a in blc.GetAllAuthors())
            {
                Console.WriteLine($"{a.Id}: {a.Name} {a.Surname}");
            }

            foreach (var b in blc.GetAllBooks())
            {
                Console.WriteLine($"{b.Id}: {b.Author.Name} {b.Author.Surname}: {b.Title}, {b.ReleaseYear}");
            }
        }
    }
}
