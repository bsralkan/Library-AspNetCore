using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Microsoft.Extensions.Primitives;

namespace Library.Controllers
{
    public class BookDetailsController : Controller
    {
        private readonly LibraryContext _context;

        public BookDetailsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookDetails.ToListAsync());
        }

        // GET: BookDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = await _context.BookDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookDetails == null)
            {
                return NotFound();
            }

            return View(bookDetails);
        }

        // GET: BookDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ISBN,BookName,ReservedDate,ReservedPerson")] BookDetails bookDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookDetails);
        }

        // GET: BookDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = await _context.BookDetails.FindAsync(id);
            if (bookDetails == null)
            {
                return NotFound();
            }
            return View(bookDetails);
        }

        // POST: BookDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ISBN,BookName,ReservedDate,ReservedPerson")] BookDetails bookDetails)
        {
            if (id != bookDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookDetailsExists(bookDetails.ID))
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
            return View(bookDetails);
        }

        // GET: BookDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = await _context.BookDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookDetails == null)
            {
                return NotFound();
            }

            return View(bookDetails);
        }

        // POST: BookDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetails = await _context.BookDetails.FindAsync(id);
            _context.BookDetails.Remove(bookDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Query()
        {
            return View();
        }
        public async Task<IActionResult> Query1(int? id)
        {
            id = Convert.ToInt32(Request.Form["ISBN"]);
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
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Register1(int? id, [Bind("ID,ISBN,BookName,ReservedDate,ReservedPerson")] BookDetails bookDetails)
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
            if(bookDetail.Register=="Returned")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(bookDetails);
                    bookDetail.Register = "Not Returned";
                    _context.Update(bookDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(bookDetails);
            }
            else
            {
                return NotFound();
            }


            
        }


        private bool BookDetailsExists(int id)
        {
            return _context.BookDetails.Any(e => e.ID == id);
        }
    }
}
