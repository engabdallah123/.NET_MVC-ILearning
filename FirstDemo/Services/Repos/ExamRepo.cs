using FirstDemo.Data;
using FirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class ExamRepo : IExamRepo
    {
        private readonly DataContext db;

        public ExamRepo(DataContext db)
        {
            this.db = db;
        }
       public Exame getExamById(int id)
        {
            return db.exames.Include(e=>e.Course).FirstOrDefault(e=>e.ExameId == id);
        }
        public Exame GetExam(int id)
        {
            return db.exames
             .Include(e => e.Questions)       
                .ThenInclude(q => q.Options)  
             .FirstOrDefault(e => e.CrsId == id);
        }
        public Exame GetExamWithQuestions(int id)
        {
            return db.exames
             .Include(e => e.Questions)
                .ThenInclude(q => q.Options)
             .FirstOrDefault(e => e.ExameId == id);
        }
        public void AddExam(Exame exam)
        {
            db.exames.Add(exam);
        }
        public void Delete(int id)
        {
            var model = db.exames.FirstOrDefault(e => e.ExameId == id);
            db.exames.Remove(model);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
