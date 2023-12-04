using Kitabi.Data;
using Kitabi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitabi.Services
{
    public class AuthorsSerivce : IAuthorsService
    {
        private readonly AppDbContext context;
        public AuthorsSerivce(AppDbContext _context)
        {
            context = _context;
        }

        public bool CheckAuthor(string authorName)
        {
            Author? c = context.Authors.FirstOrDefault(c => c.Name == authorName);
            if (c is not null)
                return false;
            return true;
        }

        public bool Confirm(int authorid)
        {
            Author author = context.Authors.SingleOrDefault(a => a.ID == authorid);
            author.Confirmed = true;
            return (context.SaveChanges()>0) ? true : false;
        }

        public void Create(string authorName,bool confirmed)
        {
            context.Authors.Add(new Author() { Name = authorName , Confirmed = confirmed});
            context.SaveChanges();
        }

        public IEnumerable<Author> GetAll()
        {
            return context.Authors.Where(a => a.Confirmed == true).ToList();
        }
        public IEnumerable<Author> GetAllRequests()
        {
            return context.Authors.Where(a => a.Confirmed == false).ToList();
        }
        public IEnumerable<SelectListItem> GetAuthorsSelectList()
        {
            return context.Authors
                .Where(a => a.Confirmed == true)
                .Select(a => new SelectListItem() { Text = a.Name, Value = a.ID.ToString() })
                .OrderBy(a => a.Text)
                .AsNoTracking()
                .ToList();
        }

        public bool Remove(int authorid)
        {
            Author author = context.Authors.SingleOrDefault(a => a.ID == authorid);
            context.Authors.Remove(author);
            return (context.SaveChanges() > 0) ? true : false;
        }
    }
}
