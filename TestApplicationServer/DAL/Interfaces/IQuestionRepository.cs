using Core.Models;

namespace DAL.Interfaces
{
    public interface IQuestionRepository
    {
        public Task<int> CountTestQuestions(int testId);
        public Task<List<Question>?> GetbyTest(int testId);
    }
}
