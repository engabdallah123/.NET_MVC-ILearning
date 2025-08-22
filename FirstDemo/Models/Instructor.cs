using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class Instructor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
       
        [RegularExpression(@"[A-Za-z0-9_]+@[A-Za-z_]+.[A-Za-z]{3,5}", ErrorMessage = "Invaild Email Pattern")]
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public Gender Gender { get; set; }
        public int DeptNo { get; set; }
        [ForeignKey("DeptNo")]
        
        
        [NotMapped]
        public IFormFile PhotoFile { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<Course> Courses { get; set; }

    }
}
