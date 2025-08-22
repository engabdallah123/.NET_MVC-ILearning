using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class CourseRepo : ICourseRepo
    {
        private readonly DataContext db;

        public CourseRepo(DataContext db)
        {
            this.db = db;
        }
        public List<Course> GetAll()
        {
            return db.courses.Include(c=>c.Instructors).ToList();
        }
        public Course GetById(int id)
        {
            return db.courses.Include(i=>i.Instructors).Include(d=>d.Departments).FirstOrDefault(c => c.CrsId == id);
        }
        public List<Instructor> GetInstructorByCrsId(int crsId)
        {
            return db.courses.Where(c => c.CrsId == crsId).Include(i => i.Instructors).SelectMany(i => i.Instructors).ToList();
        }
        public void Delete(int id)
        {
            var crs = db.courses.FirstOrDefault(c => c.CrsId == id);
            db.courses.Remove(crs);
        }
        public void Update(Course course)
        {
            db.courses.Update(course);
        }
        public void Add(Course course)
        {
            db.courses.Add(course);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        
    }
}
