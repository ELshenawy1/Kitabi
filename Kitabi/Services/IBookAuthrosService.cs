using Kitabi.Models;

namespace Kitabi.Services
{
    public interface IBookAuthrosService
    {

        IEnumerable<Book> GetAuthorBooks(int authorid);
    }
}
