namespace BookCatalogue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            BLC.BLC blc = new BLC.BLC("BookCatalogue.DAOMock.dll");

            foreach (var a in blc.GetAuthors()) 
            {
                Console.WriteLine($"{a.ID}: {a.Name} {a.Surname}");
            }

            foreach (var b in blc.GetBooks())
            {
                Console.WriteLine($"{b.ID}: {b.Author.Name} {b.Author.Surname}: {b.Title}, {b.ReleaseYear}");
            }
        }
    }
}
