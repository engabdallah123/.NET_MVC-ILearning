using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class Course
    {
        [Key]
        public int CrsId { get; set; }
        public string CrsName { get; set; }

        public int Duration { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public virtual ICollection<StudentCourse> CourseStudents { get; set; } = new List<StudentCourse>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public virtual List<Exame> Exames { get; set; } = new();
        
            
    }
}
