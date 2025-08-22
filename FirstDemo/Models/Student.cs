using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20,MinimumLength =3,ErrorMessage ="You Can't Enter This Name! Min Is 3")]
        public string Name { get; set; }
        [RegularExpression(@"[A-Za-z0-9_]+@[A-Za-z_]+.[A-Za-z]{3,5}",ErrorMessage ="Invaild Email Pattern")]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public int? DeptNumber { get; set; }
        [ForeignKey("DeptNumber")]

        [NotMapped]
        public IFormFile PhotoFile { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
