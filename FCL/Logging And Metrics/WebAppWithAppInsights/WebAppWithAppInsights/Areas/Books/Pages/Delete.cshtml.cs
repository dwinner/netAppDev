﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAppWithAppInsights.Data;

namespace WebAppWithAppInsights
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppWithAppInsights.Data.BooksContext _context;

        public DeleteModel(WebAppWithAppInsights.Data.BooksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book? Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books.FindAsync(id);

            if (Book != null)
            {
                _context.Books.Remove(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
