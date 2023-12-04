using System.ComponentModel.DataAnnotations;

namespace Kitabi.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string AllowedExtensions;
        public AllowedExtensionsAttribute(string _AllowedExtensions)
        {
            AllowedExtensions = _AllowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = AllowedExtensions.Split(",").Contains(extension.ToLower());
                if (!isAllowed) 
                    return new ValidationResult($"Only {AllowedExtensions} Ara Allowed");
            }
            return ValidationResult.Success;

        }
    }
}
