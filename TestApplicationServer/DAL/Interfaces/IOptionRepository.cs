using Core.Models;

namespace DAL.Interfaces
{
    public interface IOptionRepository
    {
        public Task<List<QuestionOption>?> GetByQuestion(int questionId);
    }
}
