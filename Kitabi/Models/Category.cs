namespace Kitabi.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public bool Confirmed { get; set; }

    }
}
