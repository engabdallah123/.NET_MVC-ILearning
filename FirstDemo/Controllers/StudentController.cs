using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FirstDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo studentRepo;
        private readonly IDepartmentRepo departmentRepo;
        public StudentController(IStudentRepo studentRepo,IDepartmentRepo departmentRepo)
        {
            this.studentRepo = studentRepo;
            this.departmentRepo = departmentRepo;
            
        }

        public IActionResult Index(string searchString)
        {
            var models = studentRepo.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                models = models.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }  
            return View(models);
        }
        public IActionResult Create()
        {
            ViewBag.Depts = departmentRepo.GetAllActive();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (student.PhotoFile != null && student.PhotoFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            student.PhotoFile.CopyTo(ms);
                            var base64 = Convert.ToBase64String(ms.ToArray());  // becuse it will be saved in Data base
                            base64 = "data:" + student.PhotoFile.ContentType + ";base64," + base64;  // save a type of image
                            student.ImageUrl = base64;
                        }

                    }
                   studentRepo.Add(student);
                    studentRepo.Save();
                    return RedirectToAction("Index");

                }
   
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);    
            }


            ViewBag.Depts = departmentRepo.GetAllActive(); // لازم تبعتها تاني
            return View(student);

        }

        public IActionResult Delete(int? id)
        {
            if (id == 0)
                return BadRequest();
            var model = studentRepo.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

           studentRepo.Delete(id);
            studentRepo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            var model = studentRepo.GetById(id.Value);
            ViewBag.Depts = departmentRepo.GetAllActive();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (student.PhotoFile != null && student.PhotoFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            student.PhotoFile.CopyTo(ms);
                            var base64 = Convert.ToBase64String(ms.ToArray());  // becuse it will be saved in Data base
                            base64 = "data:" + student.PhotoFile.ContentType + ";base64," + base64;  // save a type of image
                            student.ImageUrl = base64;
                        }

                    }
                    studentRepo.Update(student);
                    studentRepo.Save();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Depts = departmentRepo.GetAllActive(); // لازم تبعتها تاني
                return View(student);
            }

            student.Department = departmentRepo.GetById(student.DeptNumber.Value);
            ViewBag.Depts = departmentRepo.GetAllActive(); 
            return View(student);


        }

    }
  
}
