using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        private readonly ApplicationDbContext dbContext;

        private readonly ILogger<UserTestRepository> logger;
        public UserTestRepository(ApplicationDbContext applicationDbContext, ILogger<UserTestRepository> logger)
        {
            this.dbContext = applicationDbContext;
            this.logger = logger;
        }

        public async Task<List<UserTest>?> GetUserTests(string userId)
        {
            try
            {
                var userTests =  await dbContext.UserTests.Where(ut=> ut.AppUserId == userId).ToListAsync();
                return userTests;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateUserTest(UserTest userTest)
        {
            try
            {
                dbContext.UserTests.Add(userTest);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                logger.LogError($"Error happend while trying to add new UserTest: {e.Message}");
                return false;
            }
        }
    }
}
