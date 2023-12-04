﻿using Kitabi.Attributes;
using Kitabi.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Kitabi.ViewModels
{
    public class CreateBookFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;


        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;



        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; }


        [Display(Name = "Category")]
        public int CategoryID { get; set; }


        [Display(Name ="Author/s")]
        public List<int> SelectedAuthors { get; set; } = new List<int>();


        public IEnumerable<SelectListItem> Categories{ get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Authors{ get; set; } = Enumerable.Empty<SelectListItem>();




    }
}
