using System.ComponentModel.DataAnnotations;

namespace FirstDemo.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
