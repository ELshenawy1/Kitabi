using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kitabi.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [Remote("UniqueCategory" , "Categories",ErrorMessage = "This category is already defined.")]
        public string CategoryName { get; set; }
    }
}
