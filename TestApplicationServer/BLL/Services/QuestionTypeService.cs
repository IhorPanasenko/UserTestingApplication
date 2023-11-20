
using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IQuestionTypeRepository questionTypeRepository;
        private readonly ILogger<QuestionTypeService> logger;

        public QuestionTypeService(ILogger<QuestionTypeService> logger, IQuestionTypeRepository questionTypeRepository)
        {
            this.logger = logger;
            this.questionTypeRepository = questionTypeRepository;
        }

        public async Task<QuestionType?> GetById(int id)
        {
            try
            {
                return await questionTypeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
