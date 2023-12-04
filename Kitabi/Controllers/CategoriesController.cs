using Kitabi.Services;
using Kitabi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitabi.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService _categoriesService)
        {
            categoriesService = _categoriesService;
        }
        

        public IActionResult Requests()
        {
            return View(categoriesService.GetAllRequests());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Confirm(int id)
        {
            return Json(categoriesService.Confirm(id));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            return Json(categoriesService.Remove(id));
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Admin"))
                    categoriesService.Create(category.CategoryName,true);
                else
                    categoriesService.Create(category.CategoryName,false);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Index()
        {
            return View(categoriesService.GetAll());
        }
        public IActionResult UniqueCategory(string CategoryName)
        {
            return Json(categoriesService.CheckCategory(CategoryName));
        }
    }
}
