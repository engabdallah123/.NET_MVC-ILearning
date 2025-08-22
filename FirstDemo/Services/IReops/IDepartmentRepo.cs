using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface IDepartmentRepo
    {
        public List<Department> GetAllActive();
        public List<Department> GetAllNotActive();
        public Department GetById(int id);
        public List<Student> GetStuByDeptId(int id);
        public List<Course> GetCrsByDeptId(int id);
        public List<Instructor> GetInstructorByDeptId(int id);

        public void Add(Department department);
        public int MaxId();
        public void Update(Department dept, int id);
        public void Delete(int id);
        public void Save();
    }
}
