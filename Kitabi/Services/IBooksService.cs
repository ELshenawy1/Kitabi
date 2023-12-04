using Kitabi.Models;
using Kitabi.ViewModels;

namespace Kitabi.Services
{
    public interface IBooksService
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetBooksWithinCategory(int categoryid);
        Book? GetByID(int id);
        Task Edit(EditBookFormViewModel model);
        Task Create(CreateBookFormViewModel book , bool confirmed);
        bool Delete(int id);
        IEnumerable<Book> GetAllRequests();
        bool Confirm(int bookid);
    }
}
