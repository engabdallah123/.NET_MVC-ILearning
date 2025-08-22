using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface ICourseRepo
    {
        public List<Course> GetAll();
        public Course GetById(int id);
        public List<Instructor> GetInstructorByCrsId(int crsId);
        public void Delete(int id);
        public void Update(Course course);
        public void Add(Course course);
        public void Save();
    }
}
