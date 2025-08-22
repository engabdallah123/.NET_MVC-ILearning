using FirstDemo.Data;
using FirstDemo.Services.IReops;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Controllers
{
    public class DeptCourseController : Controller
    {
       private readonly IDeptCourseRepo deptCourseRepo;
        public DeptCourseController(IDeptCourseRepo deptCourseRepo)
        {
            this.deptCourseRepo = deptCourseRepo;
            
        }
        public IActionResult ShowCourseByDept(int id)
        {
            var model = deptCourseRepo.DeptCrsById(id);
            return View(model);

        }
        public IActionResult ManageCourses(int id)
        {
            var Dept = deptCourseRepo.DeptCrsById(id);
            var crsInDept = Dept.Courses;
            ViewBag.crsInDept = Dept;
            var allCourses = deptCourseRepo.AllCourses();
            var crsNotIndept = allCourses.Except(crsInDept).ToList();
            ViewBag.crsNotInDept = crsNotIndept;
            return View();
        }
        [HttpPost]
        public IActionResult ManageCourses(int id,List<int> CrsToRemove, List<int> CrsToAdd)
        {
            var model = deptCourseRepo.DeptCrsById(id);
            foreach (var item in CrsToRemove)
            {
                var crs = deptCourseRepo.GetCrsById(item);
                model.Courses.Remove(crs);
            }
            foreach(var item in CrsToAdd)
            {
                var crs = deptCourseRepo.GetCrsById(item);
                model.Courses.Add(crs);
            }
            deptCourseRepo.Save();
            return RedirectToAction("Index", "Department");
        }
    }
}
