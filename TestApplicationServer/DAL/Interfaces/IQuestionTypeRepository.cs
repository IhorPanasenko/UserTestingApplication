using Core.Models;

namespace DAL.Interfaces
{
    public interface IQuestionTypeRepository
    {
        public Task<QuestionType?> GetById(int id);
    }
}
