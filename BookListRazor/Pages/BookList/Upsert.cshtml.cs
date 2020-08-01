using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _Db;

        public UpsertModel(ApplicationDbContext Db)
        {
            _Db = Db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                //create
                return Page();
            }
            
            //update
            Book = await _Db.Book.FirstOrDefaultAsync(u=>u.Id==id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _Db.Book.Add(Book);
                }
                else
                {
                    _Db.Book.Update(Book);
                }

                await _Db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();

        }
    }
}