using System.ComponentModel.DataAnnotations;

namespace FirstDemo.ViewModel
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }

    }
}
