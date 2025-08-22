using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class InstructorRepo : IInstructorRepo
    {
        private readonly DataContext db;

        public InstructorRepo(DataContext db)
        {
            this.db = db;
        }
        public List<Instructor> GetAll()
        {
            return db.instructors.Include(d=>d.Department).ToList();
        }
        public Instructor GetById(int id)
        {
            return db.instructors.FirstOrDefault(x => x.Id == id);
        }
        public Instructor GetByName(string name)
        {
            return db.instructors.FirstOrDefault(n=>n.Name == name);
        }
        public void Add(Instructor instructor)
        {
            db.instructors.Add(instructor);
        }
        public void Update(Instructor instructor)
        {
            db.instructors.Update(instructor);
        }
        public void Delete(int id)
        {
            var inst = db.instructors.FirstOrDefault(i=>i.Id == id);
            db.instructors.Remove(inst);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
