using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookCatalogue.BLC;
using BookCatalogue.Core.DTO;
using BookCatalogue.Interfaces;
using System.Globalization;

namespace BookCatalogue.Core.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BLC.BLC _context;
        private static int _order = 1;

        public AuthorsController(BLC.BLC BLC)
        {
            _context = BLC;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string sortOrder, string currentSort, string searchStringName, DateTime? searchDateBefore, DateTime? searchDateAfter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PreviousSort"] = currentSort;
            ViewData["CurrentFilterName"] = searchStringName;
            if(searchDateBefore != null)
            {
                ViewData["CurrentFilterBornBefore"] = ((DateTime)searchDateBefore).ToString("yyyy-MM-dd");
            }
            else
            {
                ViewData["CurrentFilterBornBefore"] = searchDateBefore;
            }
            if (searchDateAfter != null)
            {
                ViewData["CurrentFilterBornAfter"] = ((DateTime)searchDateAfter).ToString("yyyy-MM-dd");
            }
            else
            {
                ViewData["CurrentFilterBornAfter"] = searchDateAfter;
            }            

            var authors = await _context.GetAllAuthorsAsync();
            var authorViewModels = authors.Select(a => new AuthorDTO
            {
                ID = a!.ID,
                Name = a.Name,
                Surname = a.Surname,
                DateOfBirth = a.DateOfBirth
            }).ToList();

            if (!string.IsNullOrEmpty(searchStringName))
            {
                authorViewModels = authorViewModels.Where(s => s.Surname.ToLower().Trim().Contains(searchStringName.ToLower().Trim())
                                       || s.Name.ToLower().Trim().Contains(searchStringName.ToLower().Trim())).ToList();
            }
            if(searchDateBefore != null)
            {
                authorViewModels = authorViewModels.Where(s => s.DateOfBirth <= searchDateBefore.Value).ToList();
            }
            if (searchDateAfter != null)
            {
                authorViewModels = authorViewModels.Where(s => s.DateOfBirth >= searchDateAfter.Value).ToList();
            }

            if (sortOrder == currentSort)
            {
                _order *= -1;
            }
            if (_order == -1)
            {
                switch (sortOrder)
                {
                    case "Name":
                        authorViewModels = [.. authorViewModels.OrderBy(s => s.Name)];
                        break;
                    case "Surname":
                        authorViewModels = [.. authorViewModels.OrderBy(s => s.Surname)];
                        break;
                    case "DateOfBirth":
                        authorViewModels = [.. authorViewModels.OrderBy(s => s.DateOfBirth)];
                        break;
                    default:
                        authorViewModels = [.. authorViewModels.OrderBy(s => s.Name)];
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "Name":
                        authorViewModels = [.. authorViewModels.OrderByDescending(s => s.Name)];
                        break;
                    case "Surname":
                        authorViewModels = [.. authorViewModels.OrderByDescending(s => s.Surname)];
                        break;
                    case "DateOfBirth":
                        authorViewModels = [.. authorViewModels.OrderByDescending(s => s.DateOfBirth)];
                        break;
                    default:
                        authorViewModels = [.. authorViewModels.OrderByDescending(s => s.Name)];
                        break;
                }
            }

            return View(authorViewModels);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.GetAuthorAsync(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            return View(ConvertToAuthorDTO(author));
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Surname,DateOfBirth")] AuthorDTO author)
        {
            if (ModelState.IsValid)
            {
                author.ID = Guid.NewGuid();
                IAuthor authorToAdd = _context.ConvertToIAuthor(author);
                _context.AddAuthor(authorToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.GetAuthorAsync(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            return View(ConvertToAuthorDTO(author));
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Surname,DateOfBirth")] AuthorDTO author)
        {
            if (id != author.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IAuthor authorToUpdate = _context.ConvertToIAuthor(author);
                    _context.UpdateAuthor(authorToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.ID))
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

            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.GetAuthorAsync(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            return View(ConvertToAuthorDTO(author));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var author = await _context.GetAuthorAsync(id);
            if (author != null)
            {
                _context.RemoveAuthor(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(Guid id)
        {
            return _context.AuthorExists(id);
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
    }
}
