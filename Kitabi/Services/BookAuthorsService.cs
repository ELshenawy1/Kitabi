using Kitabi.Data;
using Kitabi.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitabi.Services
{
    public class BookAuthorsService : IBookAuthrosService
    {
        private readonly AppDbContext context;

        public BookAuthorsService(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<Book> GetAuthorBooks(int authorid)
        {
            return context.bookAuthors
                .Include(b => b.Book)
                .Where(a => a.Book.Confirmed == true)
                .Where(b => b.AuthorID == authorid)
                .Select(b => b.Book)
                .ToList();
        }
    }
}
