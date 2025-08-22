using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Controllers
{
    public class StuCourseController : Controller
    {
       private readonly IStudentCourseRepo studentCourseRepo;
        public StuCourseController(IStudentCourseRepo studentCourseRepo)
        {
            this.studentCourseRepo = studentCourseRepo;
            
        }

        public IActionResult UpdateStudentDegree(int deptId,int crsId)
        {
            var model = studentCourseRepo.GetDeptStuById(deptId);
            ViewBag.Degree = studentCourseRepo.GetStudentCoursesById(crsId);
            ViewBag.selectedCrs = studentCourseRepo.GetCrsById(crsId);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateStudentDegree(int deptId, int crsId, Dictionary<int,int> degree)
        {
            foreach(var item in degree)
            {
                var model = studentCourseRepo.GetDegree(item.Key, crsId);
                if (model == null)
                    studentCourseRepo.Add(crsId, item.Key, item.Value);
                else
                    model.Degree = item.Value;
            }
            studentCourseRepo.Save();
            return RedirectToAction("Index", "Student");
        }
        public IActionResult ShowStudentDegree(int deptId, int crsId)
        {
            var model = studentCourseRepo.GetDeptStuById(deptId);
            ViewBag.Degree = studentCourseRepo.GetStudentCoursesById(crsId);
            ViewBag.selectedCrs = studentCourseRepo.GetCrsById(crsId);
            return View(model);
        }
    }
}
