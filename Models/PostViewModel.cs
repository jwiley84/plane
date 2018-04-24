using System.ComponentModel.DataAnnotations;
 
namespace plane.Models
{
    public class PostViewModel : BaseEntity
    {
        [Required]
        // [Display(Name = "Bright Idea:")]
        public string postContent { get; set; }
    }
}