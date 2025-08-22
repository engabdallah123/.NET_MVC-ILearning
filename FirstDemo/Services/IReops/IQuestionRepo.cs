using FirstDemo.Models;

namespace FirstDemo.Services.IReops
{
    public interface IQuestionRepo
    {
        public List<Question> GetByExamId(int examId);
        public void Add(Question question);
        public void Save();
    }
}
