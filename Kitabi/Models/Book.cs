using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kitabi.Models
{
    public class Book : BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Cover { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public bool Confirmed { get; set; }
    }
}
