using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class UserTestService: IUserTestService
    {
        private readonly IUserTestRepository userTestRepository;
        private readonly IUserRepository userRepository;
        private readonly ITestRepository testRepository;
        private readonly ILogger<UserTestService> logger;

        public UserTestService(ILogger<UserTestService> logger, IUserTestRepository userTestRepository, IUserRepository userRepository, ITestRepository testRepository)
        {
            this.logger = logger;
            this.userTestRepository = userTestRepository;
            this.userRepository = userRepository;
            this.testRepository = testRepository;
        }

        public async Task<List<UserTest>?> GetUserTests(string userId)
        {
            try
            {
                var user =  await userRepository.GetById(userId);

                if (user is null)
                {
                    throw new ArgumentException($"No user with Id {userId}");
                }

                var userTests = await userTestRepository.GetUserTests(userId);

                if(userTests is null)
                {
                    logger.LogError($"Test for user {userId} returned null");
                    return userTests;
                }

                foreach(var ut in userTests)
                {
                    ut.Test = await testRepository.GetById(ut.TestId);
                }

                return userTests;
            }
            catch (Exception ex)
            {
                logger.LogError($"Data access layer error:\n{ex.Message}");
                return null;
            }
        }
    }
}
