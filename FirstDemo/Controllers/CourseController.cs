using FirstDemo.Models;
using FirstDemo.Services.IReops;
using FirstDemo.Services.Repos;
using FirstDemo.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepo courseRepo;
        private readonly IDeptCourseRepo deptCourseRepo;
        private readonly IExamRepo examRepo;

        public CourseController(ICourseRepo courseRepo, IDeptCourseRepo deptCourseRepo,
            IExamRepo examRepo)
        {
            this.courseRepo = courseRepo;
            this.deptCourseRepo = deptCourseRepo;
            this.examRepo = examRepo;
        }
        public IActionResult Index()
        {
            
            var model = courseRepo.GetAll();
            return View(model);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = courseRepo.GetById(id.Value);
            ViewBag.depts = deptCourseRepo.GetDeptsByCrsId(id.Value);
            ViewBag.instructors = courseRepo.GetInstructorByCrsId(id.Value);
            return View(model);
           
        }
        public IActionResult CreateExam()
        {
            ViewBag.Crss = courseRepo.GetAll();
           
            return View();
        }
        //[HttpPost]
        //public IActionResult CreateExam()
        //{
        //    if(ModelState.IsValid)
        //    {
             

        //    }
        //    return View();
        //}
    }
}
