using FirstDemo.Data;
using FirstDemo.Models;
using FirstDemo.Services.IReops;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Services.Repos
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly DataContext db;

        public QuestionRepo(DataContext db)
        {
            this.db = db;
        }
        public List<Question> GetByExamId(int examId)
        {
            return db.questions
                     .Where(q => q.ExameId == examId)
                     .Include(q => q.Options) 
                     .ToList();
        }
        public void Add(Question question)
        {
            db.questions.Add(question);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
