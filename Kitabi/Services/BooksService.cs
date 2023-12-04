using Kitabi.Data;
using Kitabi.Models;
using Kitabi.Settings;
using Kitabi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Kitabi.Services
{
    public class BooksService : IBooksService
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagesPath;


        public IEnumerable<Book> GetAll()
        {
            return context.Books
                .Where(b => b.Confirmed == true)
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .AsNoTracking()
                .ToList();
        }
        public IEnumerable<Book> GetAllRequests()
        {
            return context.Books
                .Where(b => b.Confirmed == false)
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .AsNoTracking()
                .ToList();
        }

        public Book? GetByID(int id)
        {
            return context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .Include(b => b.Category)
                .AsNoTracking()
                .SingleOrDefault(b => b.ID == id);
        }

        public async Task Edit(EditBookFormViewModel model)
        {
            Book old = context.Books.Include(b=> b.BookAuthors).SingleOrDefault(b=> b.ID == model.ID);
            if (old is null)
                return;

            var hasNewCover = model.Cover is not null;
            string oldcover = "";
            old.Name = model.Name;
            old.Description = model.Description;
            old.CategoryID = model.CategoryID;
            old.BookAuthors = model.SelectedAuthors.Select(a => new BookAuthor { AuthorID = a }).ToList();

            if (hasNewCover)
            {
                oldcover = old.Cover;
                old.Cover = await SaveCover(model.Cover);
            }
            if (context.SaveChanges() > 0)
            {
                if (hasNewCover)
                    File.Delete(Path.Combine(imagesPath, oldcover));
            }
        }
        
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(imagesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;

        }

        public BooksService(AppDbContext _context, IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            webHostEnvironment = _webHostEnvironment;
            imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public async Task Create(CreateBookFormViewModel model, bool confirmed)
        {
            var coverName = await SaveCover(model.Cover);

            Book book = new()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = coverName,
                Confirmed = confirmed,
                CategoryID = model.CategoryID,
                BookAuthors = model.SelectedAuthors.Select(a => new BookAuthor { AuthorID = a }).ToList(),
            };
            context.Books.Add(book);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isdeleted = false;

            Book book = context.Books.SingleOrDefault(b => b.ID == id);
            if (book is null)
                return isdeleted;
            context.Books.Remove(book);
            if(context.SaveChanges()>0)
            {
                isdeleted = true;
                File.Delete(Path.Combine(imagesPath, book.Cover));
            }

            return isdeleted;
        }

        public IEnumerable<Book> GetBooksWithinCategory(int categoryid)
        {
            return context.Books.Where(b => b.Confirmed == true).Where(b => b.CategoryID == categoryid).ToList();
        }

        public bool Confirm(int bookid)
        {
            Book book = context.Books.SingleOrDefault(b => b.ID == bookid);
            book.Confirmed = true;
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
