using System.ComponentModel.DataAnnotations;

namespace FirstDemo.ViewModel
{
    public class RoleViewModel
    {
        [Display(Name ="Role Name:")]
        public string RoleName { get; set; }
    }
}
