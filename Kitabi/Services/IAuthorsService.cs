using Kitabi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kitabi.Services
{
    public interface IAuthorsService
    {
        bool CheckAuthor(string authorName);
        IEnumerable<Author> GetAll();
        IEnumerable<SelectListItem> GetAuthorsSelectList();
        IEnumerable<Author> GetAllRequests();
        void Create(string authorName , bool confirmed);
        bool Confirm(int authorid);
        bool Remove(int authorid);
    }
}
