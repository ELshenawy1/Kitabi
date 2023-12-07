using Kitabi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kitabi.Services
{
    public interface IAuthorsService
    {
        bool CheckAuthor(string authorName);
        IEnumerable<Author> GetAll();
        Author GetByID(int id);
        IEnumerable<SelectListItem> GetAuthorsSelectList();
        IEnumerable<Author> GetAllRequests();
        void Create(string authorName , bool confirmed);
        bool Confirm(int authorid);
        bool Remove(int authorid);
        void Edit(int id, string newName);
    }
}
