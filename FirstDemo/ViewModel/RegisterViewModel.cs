using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        
        [RegularExpression(@"[A-Za-z0-9_]+@[A-Za-z_]+.[A-Za-z]{3,5}", ErrorMessage = "Invaild Email Pattern")]
        public string Email { get; set; }
       
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Phone Number Is Required")]
        [Phone(ErrorMessage = " Phone Number Not Valid")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone Number Not Valid")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public IFormFile PhotoFile { get; set; }
        public string ImageUrl { get; set; }
    }
}
