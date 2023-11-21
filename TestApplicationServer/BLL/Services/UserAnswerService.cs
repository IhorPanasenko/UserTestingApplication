using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class UserAnswerService : IUserAnswersService
    {
        private readonly ILogger<UserAnswerService> logger;
        private readonly IUserAnswerRepository userAnswerRepository;
        private readonly IUserTestRepository userTestRepository;

        public UserAnswerService(
            IUserAnswerRepository userAnswerRepository, 
            ILogger<UserAnswerService> logger, 
            IUserTestRepository userTestRepository
            )
        {
            this.userAnswerRepository = userAnswerRepository;
            this.logger = logger;
            this.userTestRepository = userTestRepository;
        }

        public async Task<List<UserAnswer>?> GetByUSerTest(int userTestId)
        {
            try
            {
                var userTest = userTestRepository.GetById(userTestId);

                if (userTest is null)
                {
                    throw new ArgumentException($"No User Test with is {userTestId}");
                }

                return await userAnswerRepository.GetByUserTest(userTestId);
            }
            catch (Exception ex)
            {
                logger.LogError($"Data access layer error: {ex.Message}");
                return null;
            }
        }
    }
}
