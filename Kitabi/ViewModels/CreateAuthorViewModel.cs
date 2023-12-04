using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kitabi.ViewModels
{
    public class CreateAuthorViewModel
    {
        [Required]
        [Remote("UniqueAuthor", "Authors", ErrorMessage = "This Author is already defined.")]

        public string AuthorName { get; set; }
    }
}
