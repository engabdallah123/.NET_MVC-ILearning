using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class StudentRepo : IStudentRepo
    {
        private readonly DataContext db;
        public StudentRepo(DataContext db)
        {
            this.db = db;
        }
        public List<Student> GetAll()
        {
            return db.students.Include(d => d.Department).ToList();
        }
        public Student GetById(int id)
        {
            return db.students.Include(a => a.Department).FirstOrDefault(a => a.Id == id);
        }
        public void Add(Student student)
        {
            db.students.Add(student);
        }
        public void Delete(int id)
        {
            var stu = db.students.FirstOrDefault(a => a.Id == id);
            db.students.Remove(stu);
        }
        public void Update(Student student)
        {
            db.students.Update(student);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
