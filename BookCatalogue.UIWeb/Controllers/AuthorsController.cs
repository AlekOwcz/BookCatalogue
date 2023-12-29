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

namespace BookCatalogue.Core.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BLC.BLC _context;

        public AuthorsController(BLC.BLC BLC)
        {
            _context = BLC;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _context.GetAllAuthorsAsync();
            var authorViewModels = authors.Select(a => new AuthorDTO
            {
                ID = a.ID,
                Name = a.Name,
                Surname = a.Surname,
                DateOfBirth = a.DateOfBirth
            }).ToList();
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

            var authorDTO = new AuthorDTO
            {
                ID = author.ID,
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };

            return View(authorDTO);
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
            var authorDTO = new AuthorDTO
            {
                ID = author.ID,
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };
            return View(authorDTO);
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
            var authorDTO = new AuthorDTO
            {
                ID = author.ID,
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };
            return View(authorDTO);
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
    }
}
