using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface IStudentRepo
    {
        public List<Student> GetAll();
        public Student GetById(int id);
        public void Add(Student student);
        public void Delete(int id);
        public void Update(Student student);
        public void Save();
    }
}
