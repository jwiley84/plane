using System.ComponentModel.DataAnnotations;
 
namespace plane.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alias can only contain letters")]
        [Display(Name = "Alias")]
        public string alias { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "Name")]
        public string name { get; set; }
 
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must contain at least one uppercase, one lowercase, and one number.")]
        [Display(Name = "Password")]
        public string password { get; set; }
 
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string confirm { get; set; }
    }
}

