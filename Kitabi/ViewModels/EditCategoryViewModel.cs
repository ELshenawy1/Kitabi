using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kitabi.ViewModels
{
    public class EditCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }

    }
}
