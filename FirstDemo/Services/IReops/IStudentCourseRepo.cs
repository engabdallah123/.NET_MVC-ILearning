using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface IStudentCourseRepo
    {
        public Department GetDeptStuById(int id);
        public List<StudentCourse> GetStudentCoursesById(int id);
        public StudentCourse GetDegree(int StuId, int CrsId);
        public Course GetCrsById(int id);
        public void Add(int CrsId, int StuId, double Degree);
        public void Save();
    }
}
