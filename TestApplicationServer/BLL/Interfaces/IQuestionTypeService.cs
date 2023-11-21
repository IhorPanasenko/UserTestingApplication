using Core.Models;

namespace BLL.Interfaces
{
    public interface IQuestionTypeService
    {
        public Task<QuestionType?> GetById(int id);
    }
}
