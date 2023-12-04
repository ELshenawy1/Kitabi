namespace Kitabi.Models
{
    public class Author : BaseEntity
    {
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public bool Confirmed { get; set; }

    }
}
