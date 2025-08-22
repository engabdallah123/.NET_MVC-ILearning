using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Remote("CheckDeptId","Department",ErrorMessage ="This Id Is Already Exist!")]
        public int DeptId { get; set; }
        [Display(Name = "Dept Name ")]
        public string DeptName { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        
        [Display(Name ="Date Of Cereation")]
        public DateTime DateOfCreation { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }
}
