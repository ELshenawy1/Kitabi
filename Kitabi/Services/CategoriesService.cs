using Kitabi.Data;
using Kitabi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitabi.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext context;
        public CategoriesService(AppDbContext _context)
        {
            context = _context;
        }

        public bool CheckCategory(string categoryName)
        {
            Category? c = context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if(c is not null)
                return false;
            return true;
        }

        public bool Confirm(int categoryid)
        {
            Category category = context.Categories.SingleOrDefault(c => c.ID == categoryid);
            category.Confirmed = true;
            return (context.SaveChanges() > 0) ? true : false;
        }

        public void Create(string name, bool confirmed)
        {
            context.Categories.Add(new Category { Name = name , Confirmed = confirmed});
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.Where(c => c.Confirmed == true).Include(c=>c.Books).ToList();
        }

        public IEnumerable<Category> GetAllRequests()
        {
            return context.Categories.Where(c => c.Confirmed == false).ToList();
        }

        public IEnumerable<SelectListItem> GetCategoriesSelectList()
        {
            return context.Categories
                .Where(c => c.Confirmed==true)
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.ID.ToString() })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }

        public bool Remove(int categoryid)
        {
            Category category = context.Categories.SingleOrDefault(c=> c.ID == categoryid);
            context.Categories.Remove(category);
            return (context.SaveChanges() > 0) ? true : false;
        }
    }
}
