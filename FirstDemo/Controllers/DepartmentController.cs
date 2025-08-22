using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Controllers
{
    public class DepartmentController : Controller
    {
      private readonly IDepartmentRepo departmentRepo;
        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var models = departmentRepo.GetAllActive();
            return View(models);
        }
        public IActionResult NotActive()
        {
            var models = departmentRepo.GetAllNotActive();
            return View(models);
        }
        
        public IActionResult Active(int id)
        {
            var model = departmentRepo.GetById(id);
            model.IsActive = true;
            departmentRepo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    departmentRepo.Add(department);
                    departmentRepo.Save();
                    return RedirectToAction("Index");

                }
            }catch(Exception ex)
            {
                int newId = departmentRepo.MaxId() + 100;
                ModelState.AddModelError("DeptId", $"Sorry Man You Can't Enter This ID, You Can Enter {newId}");
            }
            return View("create");

        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = departmentRepo.GetById(id.Value);
            if (model == null)
                return NotFound();

            ViewBag.Students = departmentRepo.GetStuByDeptId(id.Value);
            ViewBag.Courses = departmentRepo.GetCrsByDeptId(id.Value);
            ViewBag.Instructors = departmentRepo.GetInstructorByDeptId(id.Value);
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            if(id == 0)
                return BadRequest();
            var model = departmentRepo.GetById(id.Value);
            if (model == null)
                return NotFound();
            model.IsActive = true;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Department dept, int id)
        {
            
            dept.DeptId = id;
           departmentRepo.Update(dept,id);
            departmentRepo.Save();
            return RedirectToAction("Index");
        }
   
        public IActionResult Delete(int? id)
        {
            if(id == 0)
                return BadRequest();
            var model = departmentRepo.GetById(id.Value);
            if(model == null) 
                return NotFound();
            ViewBag.Students = departmentRepo.GetStuByDeptId(id.Value);
            ViewBag.Courses = departmentRepo.GetCrsByDeptId(id.Value);
            ViewBag.Instructors = departmentRepo.GetInstructorByDeptId(id.Value);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

            var dept = departmentRepo.GetById(id);
            dept.IsActive = false;
            departmentRepo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult CheckDeptId(int id)
        {
            var dept = departmentRepo.GetById(id);
            if (dept == null)
                return Json(true);
            else
            {
                int newId = departmentRepo.MaxId() + 100;
                return Json($"This ID Not Valid You Can Use {newId}");
            }
        }
      
    }
}
