using Kitabi.Services;
using Kitabi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kitabi.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorService;
        private readonly IBookAuthrosService bookAuthrosService;

        public AuthorsController(IAuthorsService _authorService,
            IBookAuthrosService _bookAuthrosService)
        {
            authorService = _authorService;
            bookAuthrosService = _bookAuthrosService;
        }


        public IActionResult Requests()
        {
            return View(authorService.GetAllRequests());
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditAuthorViewModel vm = new () { AuthorName = authorService.GetByID(id).Name };
            return View(vm);
        }


        [HttpPost]
        public IActionResult Edit(int id, string AuthorName)
        {
            if (ModelState.IsValid)
            {
                authorService.Edit(id, AuthorName);
                 return RedirectToAction("IndexAdminView");
            }
            return View(new EditAuthorViewModel() { AuthorName = AuthorName});
        }


        public IActionResult IndexAdminView()
        {
            return View(authorService.GetAll());
        }


        public IActionResult Index()
        {
            return View(authorService.GetAll());
        }


        public IActionResult GetAuthorBooks(int id)
        {
            return Json(bookAuthrosService.GetAuthorBooks(id));
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CreateAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                authorService.Create(author.AuthorName, User.IsInRole("Admin") ? true : false);
                return RedirectToAction("Index");
            }
            return View(author);
        }


        public IActionResult UniqueAuthor(string AuthorName)
        {
            return Json(authorService.CheckAuthor(AuthorName));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Confirm(int id)
        {
            return Json(authorService.Confirm(id));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            return Json(authorService.Remove(id));
        }
    }
}
