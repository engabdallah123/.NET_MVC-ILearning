using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int StuId { get; set; }
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public double Degree { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
