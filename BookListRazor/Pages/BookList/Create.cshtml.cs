using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _Db;

        public CreateModel(ApplicationDbContext Db)
        {
            _Db = Db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _Db.Book.AddAsync(Book);
                await _Db.SaveChangesAsync();
                return RedirectToPage("Index"); 

            }
            else
            {
                return Page();
            }
        }
    }
}