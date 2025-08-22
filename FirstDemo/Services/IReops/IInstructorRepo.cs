using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface IInstructorRepo
    {
        public List<Instructor> GetAll();
        public Instructor GetById(int id);
        public Instructor GetByName(string name);
        public void Add(Instructor instructor);
        public void Update(Instructor instructor);
        public void Delete(int id);
        public void Save();

    }
}
