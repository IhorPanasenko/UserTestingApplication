using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository optionRepository;
        private readonly ILogger<OptionService> logger;

        public OptionService(ILogger<OptionService> logger, IOptionRepository optionRepository)
        {
            this.logger = logger;
            this.optionRepository = optionRepository;
        }

        public async Task<List<QuestionOption>?> GetByQuestion(int questionId)
        {
            try
            {
                return await optionRepository.GetByQuestion(questionId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
