using Kitabi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Kitabi.Services
{
    public interface ICategoriesService
    {
        void Create(string name , bool confirmed);
        bool CheckCategory(string categoryName);
        IEnumerable<Category> GetAllRequests();
        IEnumerable<Category> GetAll();
        IEnumerable<SelectListItem> GetCategoriesSelectList();
        bool Confirm(int cotegoryid);
        bool Remove(int categoryid);
    }
}
