using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookCatalogue.BLC;
using BookCatalogue.UIWeb.Models;

namespace BookCatalogue.UIWeb.Controllers
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
            var authors = _context.GetAuthors();
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
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var author = await _context.Authors
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (author == null)
            //{
            //    return NotFound();
            //}

            //return View(author);
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
            throw new NotImplementedException();
            //if (ModelState.IsValid)
            //{
            //    author.ID = Guid.NewGuid();
            //    _context.Add(author);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var author = await _context.Authors.FindAsync(id);
            //if (author == null)
            //{
            //    return NotFound();
            //}
            //return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Surname,DateOfBirth")] AuthorDTO author)
        {
            throw new NotImplementedException();
            //if (id != author.ID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(author);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!AuthorExists(author.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var author = await _context.Authors
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (author == null)
            //{
            //    return NotFound();
            //}

            //return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            throw new NotImplementedException();
            //var author = await _context.Authors.FindAsync(id);
            //if (author != null)
            //{
            //    _context.Authors.Remove(author);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(Guid id)
        {
            throw new NotImplementedException();
            //return _context.Authors.Any(e => e.ID == id);
        }
    }
}
