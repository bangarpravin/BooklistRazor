using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _Db;

        public EditModel(ApplicationDbContext Db)
        {
            _Db = Db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book = await _Db.Book.FindAsync(id); 
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _Db.Book.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author  = Book.Author;
                BookFromDb.ISBN= Book.ISBN;

                await _Db.SaveChangesAsync();

                return RedirectToPage("Index"); 
            }
            return RedirectToPage();
        
        }
    }
}