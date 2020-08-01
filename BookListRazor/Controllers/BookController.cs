using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _Db;

        public BookController(ApplicationDbContext Db)
        {
            _Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _Db.Book.ToListAsync() });
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _Db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _Db.Book.Remove(bookFromDb);
            await _Db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
