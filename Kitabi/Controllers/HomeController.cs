using Kitabi.Models;
using Kitabi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kitabi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksService booksService;

        public HomeController(IBooksService _booksService)
        {
            booksService = _booksService;
        }

        public IActionResult Index()
        {
            return View(booksService.GetAll());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}