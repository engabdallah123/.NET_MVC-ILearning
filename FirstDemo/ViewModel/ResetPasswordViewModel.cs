using System.ComponentModel.DataAnnotations;

namespace FirstDemo.ViewModel
{
    public class ResetPasswordViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }


    }
}
