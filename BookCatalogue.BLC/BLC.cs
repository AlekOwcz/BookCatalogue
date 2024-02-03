using System.Reflection;
using BookCatalogue.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogue.BLC
{
    public class BLC
    {
        private static BLC? _instance;
        private readonly IDAO? _dao;


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

        public Task<int> SaveChangesAsync()
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.SaveChangesAsync();
        }


        public IAuthor? GetAuthor(Guid id) 
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.GetAuthor(id);
        }

        public async Task<IAuthor?> GetAuthorAsync(Guid id)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return await _dao.GetAuthorAsync(id);
        }

        public IEnumerable<IAuthor?> GetAllAuthors()
        {
            if( _dao == null ) 
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.GetAllAuthors();
        }
        
        public async Task<IEnumerable<IAuthor?>> GetAllAuthorsAsync()
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return await _dao.GetAllAuthorsAsync();
        }

        public IBook? GetBook(Guid id)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.GetBook(id);
        }
        
        public async Task<IBook?> GetBookAsync(Guid id)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return await _dao.GetBookAsync(id);
        }
        
        public IEnumerable<IBook?> GetAllBooks()
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.GetAllBooks();
        }
        
        public async Task<IEnumerable<IBook?>> GetAllBooksAsync()
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return await _dao.GetAllBooksAsync();
        }


        public void AddAuthor(IAuthor author)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            _dao.AddAuthor(author);
        }

        public void AddBook(IBook book)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            _dao.AddBook(book);
        }


        public void UpdateAuthor(IAuthor author)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            _dao.UpdateAuthor(author);
        }
        public void UpdateBook(IBook book)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            _dao.UpdateBook(book);
        }


        public void RemoveAuthor(IAuthor author)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            _dao.DeleteAuthor(author);
        }
        public void RemoveBook(IBook book)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            _dao.DeleteBook(book);
        }


        public bool AuthorExists(Guid id)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.AuthorExists(id);
        }
        public bool BookExists(Guid id)
        {
            if (_dao == null)
            {
                throw new Exception($"No instance of DAO found");
            }
            return _dao.BookExists(id);
        }
    }
}
