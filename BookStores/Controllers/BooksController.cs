using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStores.Models;

namespace BookStores.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookStoreContext _context;

        public BooksController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.BookDetails != null ? 
                          View(await _context.BookDetails.ToListAsync()) :
                          Problem("Entity set 'BookStoreContext.BookDetails'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BookDetails == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.BookDetails
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (bookDetail == null)
            {
                return NotFound();
            }

            return View(bookDetail);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,AuthorName,Isbn,Cost,Genre")] BookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookDetail);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BookDetails == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.BookDetails.FindAsync(id);
            if (bookDetail == null)
            {
                return NotFound();
            }
            return View(bookDetail);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title,AuthorName,Isbn,Cost,Genre")] BookDetail bookDetail)
        {
            if (id != bookDetail.Isbn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookDetailExists(bookDetail.Isbn))
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
            return View(bookDetail);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.BookDetails == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.BookDetails
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (bookDetail == null)
            {
                return NotFound();
            }

            return View(bookDetail);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.BookDetails == null)
            {
                return Problem("Entity set 'BookStoreContext.BookDetails'  is null.");
            }
            var bookDetail = await _context.BookDetails.FindAsync(id);
            if (bookDetail != null)
            {
                _context.BookDetails.Remove(bookDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookDetailExists(string id)
        {
          return (_context.BookDetails?.Any(e => e.Isbn == id)).GetValueOrDefault();
        }
    }
}
