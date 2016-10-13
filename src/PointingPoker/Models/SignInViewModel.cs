using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
