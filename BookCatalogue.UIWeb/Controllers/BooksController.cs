using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookCatalogue.Core.DTO;
using BookCatalogue.UIWeb.Data;
using BookCatalogue.Interfaces;

namespace BookCatalogue.UIWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly BLC.BLC _context;

        public BooksController(BLC.BLC BLC)
        {
            _context = BLC;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.GetAllBooksAsync();
            var booksViewModels = books.Select(b => new BookDTO
            {
                ID = b.ID,
                Title = b.Title,
                ReleaseYear = b.ReleaseYear,
                Author = ConvertToAuthorDTO(b.Author),
                Language = b.Language,
                Genre = b.Genre
            }).ToList();
            return View(booksViewModels);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.GetBookAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(ConvertToBookDTO(book));
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            PopulateAuthorsDropDownList(null);
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseYear,AuthorID,Language,Genre")] BookDTO book)
        {
            ModelState.Remove("Author");
            if (ModelState.IsValid)
            {
                book.ID = Guid.NewGuid();
                IBook bookToAdd = _context.ConvertToIBook(book);
                _context.AddBook(bookToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAuthorsDropDownList(null);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.GetBookAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }
            var bookToUpdate = ConvertToBookDTO(book);
            PopulateAuthorsDropDownList(bookToUpdate.AuthorID);
            return View(bookToUpdate);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Title,ReleaseYear,AuthorID,Language,Genre")] BookDTO book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }
            ModelState.Remove("Author");
            if (ModelState.IsValid)
            {
                try
                {
                    IBook bookToUpdate = _context.ConvertToIBook(book);
                    if (bookToUpdate.ReleaseYear <= bookToUpdate.Author.DateOfBirth.Year) 
                    {
                        ModelState.AddModelError("ReleaseYear", "The release year can not be before author's birth.");
                    }
                    if (!ModelState.IsValid) 
                    {
                        PopulateAuthorsDropDownList(book.AuthorID);
                        return View(book);
                    }
                    _context.UpdateBook(bookToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookDTOExists(book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateAuthorsDropDownList(book.AuthorID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.GetBookAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(ConvertToBookDTO(book));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var book = await _context.GetBookAsync(id);
            if (book != null)
            {
                _context.RemoveBook(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookDTOExists(Guid id)
        {
            return _context.BookExists(id);
        }

        private AuthorDTO ConvertToAuthorDTO(IAuthor author)
        {
            AuthorDTO authorDTO = new()
            {
                ID = author.ID,
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };
            return authorDTO;
        }

        private BookDTO ConvertToBookDTO(IBook book)
        {
            BookDTO bookDTO = new()
            {
                ID = book.ID,
                Title = book.Title,
                ReleaseYear = book.ReleaseYear,
                AuthorID = book.Author.ID,
                Author = ConvertToAuthorDTO(book.Author),
                Language = book.Language,
                Genre = book.Genre
            };
            return bookDTO;
        }

        private void PopulateAuthorsDropDownList(Guid? selectedAuthor)
        {
            IEnumerable<IAuthor?> authors = _context.GetAllAuthors();
            var authorViewModels = authors.Select(a => new AuthorDTO
            {
                ID = a.ID,
                Name = a.Name,
                Surname = a.Surname,
                DateOfBirth = a.DateOfBirth
            }).OrderBy(a => a.Surname).ThenBy(a => a.Name).ToList();
            authorViewModels.Insert(0, new AuthorDTO { ID = Guid.Empty, Name = "Select an author", Surname = "", DateOfBirth = new DateTime() });
            ViewData["Authors"] = new SelectList(authorViewModels, "ID", "FullName", selectedAuthor);
        }
    }
}
