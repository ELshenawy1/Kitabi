using System.ComponentModel.DataAnnotations;

namespace Kitabi.ViewModels
{
    public class EditAuthorViewModel
    {
        [Required]

        public string AuthorName { get; set; }

    }
}
