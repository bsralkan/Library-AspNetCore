using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.BookDetails.ToListAsync();
            List<BookDetails> bookd = new List<BookDetails>();
            foreach (var item in books)
            {
                if (item.ISBN == id)
                {
                    bookd.Add(item);
                }

            }
                
            if (books == null)
            {
                return NotFound();
            }
            
            return View(bookd);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ISBN,Name,Author,Register,LastCheckedDate,LastCheckedPerson")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ISBN,Name,Author,Register,LastCheckedDate,LastCheckedPerson")] Books books)
        {
            if (id != books.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.ID))
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
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.ID == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Status()
        {
            return View();
        }
        
        public async Task<IActionResult> Status1(int id)
        {
            id = Convert.ToInt32(Request.Form["ISBN"]);
            var bookDetails = await _context.Books
                .FirstOrDefaultAsync(m => m.ISBN == id);

            await _context.SaveChangesAsync();
            return View(bookDetails);
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut1(int? id)
        {
            id = Convert.ToInt32(Request.Form["ISBN"]);
            if (id == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.Books
                .FirstOrDefaultAsync(m => m.ISBN == id);

            if (bookDetail == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                bookDetail.Register = "Returned";
                _context.Update(bookDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }     




            private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.ID == id);
        }
    }
}
