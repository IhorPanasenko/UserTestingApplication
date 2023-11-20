using Core.Models;

namespace BLL.Interfaces
{
    public interface IOptionService
    {
        public Task<List<QuestionOption>?> GetByQuestion(int questionId);
    }
}
