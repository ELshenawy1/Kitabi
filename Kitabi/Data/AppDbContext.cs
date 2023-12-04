using Kitabi.Models;
using Kitabi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kitabi.Data
{
    public class AppDbContext : IdentityDbContext //DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> bookAuthors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category{ID = 1 , Name = "Fiction"},
                new Category{ID = 2 , Name = "Non-Fiction"},
                new Category{ID = 3 , Name = "Thriller"},
                new Category{ID = 4 , Name = "Science Fiction"},
                new Category{ID = 5 , Name = "Fantasy"},
                new Category{ID = 6 , Name = "Romance"},
                new Category{ID = 7 , Name = "Science"}
            });
            modelBuilder.Entity<Author>().HasData(new Author[]
            {
                new Author{ID = 1 , Name = "William Shakespeare"},
                new Author{ID = 2 , Name = "James Clear"},
                new Author{ID = 3 , Name = "Robert T. Kiyosaki"},
            });
            modelBuilder.Entity<BookAuthor>().HasKey(b => new { b.AuthorID, b.BookID });
            base.OnModelCreating(modelBuilder);
        }
    }
}
