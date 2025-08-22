using FirstDemo.Models;
using FirstDemo.Services.IReops;
using FirstDemo.Services.Repos;
using FirstDemo.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepo examRepo;
        private readonly ICourseRepo courseRepo;
        private readonly IQuestionRepo questionRepo;

        public ExamController(IExamRepo examRepo,ICourseRepo courseRepo,IQuestionRepo questionRepo)
        {
            this.examRepo = examRepo;
            this.courseRepo = courseRepo;
            this.questionRepo = questionRepo;
        }
        public IActionResult AddExam()
        {
            ViewBag.Courses = courseRepo.GetAll();
            return View();
        }
        [HttpPost] 
        public IActionResult AddExam(Exame exam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Crs = courseRepo.GetById(exam.CrsId);
                    exam.ExameName = Crs.CrsName;
                    examRepo.AddExam(exam);
                    examRepo.Save();
                    return RedirectToAction("AddQuestion",new { examId = exam.ExameId});
                }else
                {
                    ViewBag.Courses = courseRepo.GetAll();
                    return View();
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Courses = courseRepo.GetAll();
                return View();
            }
           
           
        }
        public IActionResult AddQuestion(int examId)
        {
          ViewBag.Exame = examRepo.getExamById(examId);

            var question = new Question
            {
                ExameId = examId,
                Options = new List<Option>
                {
                new Option(),
                new Option(),
                new Option(),
                new Option()
                }
            };
            return View(question);
        }
        [HttpPost]
        public IActionResult AddQuestion(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    questionRepo.Add(question);
                    questionRepo.Save();
                    TempData["QuestionAddMessage"] = "Question Added successfully! ✅";
                    return RedirectToAction("AddQuestion", new { examId = question.ExameId });  // add anther question
                }else
                {
                    question.Exame = examRepo.getExamById(question.ExameId);
                    return View(question);
                }
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                question.Exame = examRepo.getExamById(question.ExameId);
                return View(question);
            }
        }
        public IActionResult ShowExam()
        {
            ViewBag.Courses = courseRepo.GetAll();
            return View(new Exame());
        }
        [HttpPost]
        public IActionResult ShowExam(Exame exam)
        {
            var exame = examRepo.GetExam(exam.CrsId);

            if (exame == null)
            {
                ModelState.AddModelError("", "No exam found for this course");
                ViewBag.Courses = courseRepo.GetAll();
                return View();
            }

         
            var questions = questionRepo.GetByExamId(exame.ExameId);

            ViewBag.Exam = exame;
            return View("ExamDetails", questions);
        }
        public IActionResult ExamDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitExam(int examId, Dictionary<int, int> answers)
        {
            var exam = examRepo.GetExamWithQuestions(examId);
            if (exam == null)
            {
                return NotFound("Exam not found");
            }

            if (answers == null)  
                answers = new Dictionary<int, int>();

            int score = 0;

            foreach (var question in exam.Questions)
            {
                if (answers.TryGetValue(question.QuestionId, out int chosenOptionId))
                {
                    if (question.Options.Any(o => o.OptionId == chosenOptionId && o.IsCorrect))
                    {
                        score++;
                    }
                }
            }

            var result = new ExamResultViewModel
            {
                ExamName = exam.ExameName,
                FullMark = exam.FullMark,
                Score = score
            };

            return View("ExamResult", result);
        }
        public IActionResult ExamResult()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteExam(int id)
        {
            examRepo.Delete(id);
            examRepo.Save();
            TempData["deleteExamMessage"] = "Exam Deleted successfully! ✅";
            return RedirectToAction("Index","department");
        }

    }
}
