using BookCatalogue.DAOSQL;
using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BookCatalogue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
 

            //string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"];

            //BLC.BLC blc = BLC.BLC.GetInstance(libraryName);

            //foreach (var a in blc.GetAuthors()) 
            //{
            //    Console.WriteLine($"{a.ID}: {a.Name} {a.Surname}");
            //}

            //foreach (var b in blc.GetBooks())
            //{
            //    Console.WriteLine($"{b.ID}: {b.Author.Name} {b.Author.Surname}: {b.Title}, {b.ReleaseYear}");
            //}
        }
    }
}
