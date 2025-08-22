using FirstDemo.Models;

namespace FirstDemo.Services.Repos
{
    public interface IExamRepo
    {
        public Exame getExamById(int id);
        public Exame GetExam(int id);
        public Exame GetExamWithQuestions(int id);
        public void AddExam(Exame exam);
        public void Delete(int id);
        public void Save();
    }
}
