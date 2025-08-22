using Microsoft.AspNetCore.Identity;

namespace FirstDemo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
