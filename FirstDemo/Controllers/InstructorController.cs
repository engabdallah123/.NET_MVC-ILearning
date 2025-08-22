using FirstDemo.Models;
using FirstDemo.Services.IReops;
using FirstDemo.Services.Repos;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepo instructorRepo;
        private readonly IDepartmentRepo departmentRepo;

        public InstructorController(IInstructorRepo instructorRepo,IDepartmentRepo departmentRepo)
        {
            this.instructorRepo = instructorRepo;
            this.departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var model = instructorRepo.GetAll();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Depts = departmentRepo.GetAllActive();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (instructor.PhotoFile != null && instructor.PhotoFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            instructor.PhotoFile.CopyTo(ms);
                            var base64 = Convert.ToBase64String(ms.ToArray());  // becuse it will be saved in Data base
                            base64 = "data:" + instructor.PhotoFile.ContentType + ";base64," + base64;  // save a type of image
                            instructor.ImageUrl = base64;
                        }

                    }
                    instructorRepo.Add(instructor);
                    instructorRepo.Save();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }


            ViewBag.Depts = departmentRepo.GetAllActive(); // لازم تبعتها تاني
            return View(instructor);

        }
        public IActionResult Delete(int? id)
        {
            if (id == 0)
                return BadRequest();
            var model = instructorRepo.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {

            instructorRepo.Delete(id.Value);
            instructorRepo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            var model = instructorRepo.GetById(id.Value);
            ViewBag.Depts = departmentRepo.GetAllActive();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (instructor.PhotoFile != null && instructor.PhotoFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            instructor.PhotoFile.CopyTo(ms);
                            var base64 = Convert.ToBase64String(ms.ToArray());  // becuse it will be saved in Data base
                            base64 = "data:" + instructor.PhotoFile.ContentType + ";base64," + base64;  // save a type of image
                            instructor.ImageUrl = base64;
                        }

                    }
                    instructorRepo.Update(instructor);
                   instructorRepo.Save();
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }


            ViewBag.Depts = departmentRepo.GetAllActive(); // لازم تبعتها تاني
            return View(instructor);


        }
    }
}
