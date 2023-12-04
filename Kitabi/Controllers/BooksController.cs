using Kitabi.Data;
using Kitabi.Models;
using Kitabi.Services;
using Kitabi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kitabi.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext context;
        private readonly ICategoriesService categoriesService;
        private readonly IAuthorsService authorsService;
        private readonly IBooksService booksService;


        public BooksController(AppDbContext _context,
            ICategoriesService _categoriesService,
            IAuthorsService _authorsService,
            IBooksService _booksService)
        {
            context = _context;
            categoriesService = _categoriesService;
            authorsService = _authorsService;
            booksService = _booksService;
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(booksService.GetAll());
        }


        [Authorize(Roles ="Admin")]
        public IActionResult Requests()
        {
            return View(booksService.GetAllRequests());
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Confirm(int id)
        {
            bool confirmed = booksService.Confirm(id);
            return Json(confirmed);
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            Book book = booksService.GetByID(id);
            if (book is null)
                return NotFound();

            EditBookFormViewModel viewModel = new()
            {
                ID = id,
                Name = book.Name,
                CategoryID = book.CategoryID,
                Description = book.Description,
                SelectedAuthors = book.BookAuthors.Select(b => b.AuthorID).ToList(),
                Authors = authorsService.GetAuthorsSelectList(),
                Categories = categoriesService.GetCategoriesSelectList(),
                CoverName = book.Cover
            };
            return View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditBookFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Authors = authorsService.GetAuthorsSelectList();
                viewModel.Categories = categoriesService.GetCategoriesSelectList();
                return View(viewModel);
            }
            else
            {
                booksService.Edit(viewModel);
                return RedirectToAction("Index");
            }
        }


        public IActionResult Details(int id)
        {
            Book book = booksService.GetByID(id);
            if (book is null)
                return NotFound();
            return View(book);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var categories = context.Categories.ToList();
            CreateBookFormViewModel ViewModel = new()
            {
                Categories = categoriesService.GetCategoriesSelectList(),
                Authors = authorsService.GetAuthorsSelectList()
            }; 
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoriesService.GetCategoriesSelectList();
                model.Authors = authorsService.GetAuthorsSelectList();
                return View(model);
            }
            if (User.IsInRole("Admin"))
                await booksService.Create(model,true);
            else
                await booksService.Create(model,false);

            return RedirectToAction(nameof(Index), "Home");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var isdeleted = booksService.Delete(id);
            if (isdeleted)
                return Ok();
            else
                return BadRequest();
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult GetBooksWithinCategory(int id)
        {
            return Json(booksService.GetBooksWithinCategory(id));
        }

    }
}
