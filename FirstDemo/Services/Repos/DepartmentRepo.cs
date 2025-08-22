using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly DataContext db;
        public DepartmentRepo(DataContext db)
        {
            this.db = db;
        }
        public List<Department> GetAllActive()
        {
            return db.departments.Where(a => a.IsActive == true).Include(s => s.Students).ToList();
        }
        public List<Department> GetAllNotActive()
        {
            return db.departments.Where(a => a.IsActive == false).Include(s => s.Students).ToList();
        }
        public Department GetById(int id)
        {
            return db.departments.FirstOrDefault(a => a.DeptId == id);
        }
        public List<Student> GetStuByDeptId(int id)
        {
            return db.departments.Where(d => d.DeptId == id).Include(s => s.Students).SelectMany(s => s.Students).ToList();
        }
        public List<Course> GetCrsByDeptId(int id)
        {
            return db.departments.Where(d => d.DeptId == id).Include(s => s.Courses).SelectMany(s => s.Courses).ToList();
        }
        public List<Instructor> GetInstructorByDeptId(int id)
        {
            return db.departments.Where(d => d.DeptId == id).Include(s => s.Instructors).SelectMany(s => s.Instructors).ToList();
        }
        public void Add(Department department)
        {
            db.departments.Add(department);
        }
        public int MaxId()
        {
            return db.departments.Max(d => d.DeptId);
        }
        public void Update(Department dept,int id)
        {
            dept.DeptId = id;
            db.departments.Update(dept);
        }
        public void Delete(int id)
        {
          var model = db.departments.FirstOrDefault(a => a.DeptId == id);
           // db.departments.Remove(model);  // hard delete
           model.IsActive = false; // soft delete
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
