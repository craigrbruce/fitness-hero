using System.ComponentModel.DataAnnotations;

namespace Server.Models.AccountViewModels
{
    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Stay signed in")]
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}