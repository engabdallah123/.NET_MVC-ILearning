using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class DeptCourseRepo : IDeptCourseRepo
    {
        private readonly DataContext db;
        public DeptCourseRepo(DataContext db)
        {
            this.db = db;
        }
        public List<Course> AllCourses()
        {
            return db.courses.ToList();
        }
        public Course GetCrsById(int id)
        {
            return db.courses.FirstOrDefault(a => a.CrsId == id);
        }
        public Department DeptCrsById(int id)
        {
            return db.departments.Include(c => c.Courses).FirstOrDefault(a => a.DeptId == id);
        }
        public List<Department> GetDeptsByCrsId(int crsId)
        {
            return db.courses.Where(c=>c.CrsId == crsId).Include(d=>d.Departments).SelectMany(d=>d.Departments).ToList();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
