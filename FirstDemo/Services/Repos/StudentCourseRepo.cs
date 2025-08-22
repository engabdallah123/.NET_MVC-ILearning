using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class StudentCourseRepo : IStudentCourseRepo
    {
        private readonly DataContext db;
        public StudentCourseRepo(DataContext db)
        {
            this.db = db;
            
        }
        public Department GetDeptStuById(int id)
        {
            return db.departments.Include(s => s.Students).FirstOrDefault(a => a.DeptId == id);
        }
        public List<StudentCourse> GetStudentCoursesById(int id)
        {
            return db.studentCourses
                   .Where(sc => sc.CrsId == id)
                   .ToList();
        }
        public StudentCourse GetDegree(int StuId,int CrsId)
        {
            return db.studentCourses.FirstOrDefault(a => a.StuId == StuId && a.CrsId == CrsId);
        }
        public Course GetCrsById(int id)
        {
            return db.courses.FirstOrDefault(c => c.CrsId == id);
        }
        public void Add(int CrsId,int StuId,double Degree)
        {
            db.studentCourses.Add(new StudentCourse()
            {
                StuId = StuId,
                CrsId = CrsId,
                Degree =Degree,
            });
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
