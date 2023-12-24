using System.Reflection;
using BookCatalogue.Interfaces;

namespace BookCatalogue.BLC
{
    public class BLC
    {
        private static BLC? _instance = null;
        private IDAO? _dao = null;

        private BLC(string libraryName)
        {
            Type? typeToCreate = null;

            try
            {
                Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);

                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAssignableTo(typeof(IDAO)))
                    {
                        typeToCreate = type;
                        break;
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the library {libraryName}: {ex.Message}");
            }


            if (typeToCreate == null)
            {
                Console.WriteLine("No type assignable to IDAO was found in the assembly.");
                return;
            }

            _dao = Activator.CreateInstance(typeToCreate, null) as IDAO;

            if (_dao == null)
            {
                Console.WriteLine($"Failed to create an instance of the type {typeToCreate.Name}.");
                return;
            }
        }
        public static BLC GetInstance(string libraryName)
        {
            if (_instance == null) 
            { 
                _instance = new BLC(libraryName);
            }
            if (_instance == null) 
            {
                throw new Exception($"Failed to create an instance of {libraryName}.");
            }
            return _instance;
        }

        public IEnumerable<IAuthor> GetAuthors()
        {
            if( _dao == null ) 
            { 
                return new List<IAuthor>();  
            }
            return _dao.GetAllAuthors();
        }

        public IEnumerable<IBook> GetBooks()
        {
            if (_dao == null)
            {
                return new List<IBook>();
            }
            return _dao.GetAllBooks();
        }
    }
}
