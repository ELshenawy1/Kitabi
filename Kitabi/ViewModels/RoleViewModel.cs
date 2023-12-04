using System.ComponentModel.DataAnnotations;

namespace Kitabi.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
