using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface IDeptCourseRepo
    {
        public List<Course> AllCourses();
        public Course GetCrsById(int id);
        public Department DeptCrsById(int id);
        public List<Department> GetDeptsByCrsId(int crsId);
        public void Save();
    }
}
