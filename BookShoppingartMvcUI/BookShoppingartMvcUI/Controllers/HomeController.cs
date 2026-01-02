using BookShoppingartMvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using BookShoppingartMvcUI.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookShoppingartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _context.Database.SetCommandTimeout(300);
        }
        
        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {
            var booksQuery = _context.Books.Include(b => b.Genre).AsQueryable();

            // Search by title or author
            if (!string.IsNullOrEmpty(sterm))
            {
                sterm = sterm.ToLower();
                booksQuery = booksQuery.Where(b => b.BookName.ToLower().Contains(sterm) ||
                                                  b.AuthorName.ToLower().Contains(sterm));
            }

            // Filter by genre
            if (genreId > 0)
            {
                booksQuery = booksQuery.Where(b => b.GenreId == genreId);
            }

            var books = await booksQuery.ToListAsync();
            var genres = await _context.Genres.ToListAsync();

            // ViewBag দিয়ে View-তে পাঠাও
            ViewBag.Books = books;
            ViewBag.Genres = genres;
            ViewBag.STerm = sterm;
            ViewBag.GenreId = genreId;

            return View();
        }
    


         public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
