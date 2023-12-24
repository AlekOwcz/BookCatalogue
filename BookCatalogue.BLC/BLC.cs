using BookCatalogue.Interfaces;
using System.Reflection;

namespace BookCatalogue.BLC
{
    public class BLC
    {
        private IDAO dao;
        //statyczna metoda get instance, sprawdzic czy obiekt pusty
        public BLC(string libraryName)
        {
            Type? typeToCreate = null;
            //todo try catch 
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO))) 
                {
                    typeToCreate = type;
                    break;
                }
            }

            //todo null check

            dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
        }

        public IEnumerable<IAuthor> GetAuthors()
        {
            return dao.GetAllAuthors();
        }

        public IEnumerable<IBook> GetBooks()
        {
            return dao.GetAllBooks();
        }
    }
}
