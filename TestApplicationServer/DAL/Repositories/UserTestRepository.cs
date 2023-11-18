using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DAL.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly ILogger<UserTestRepository> logger;
        public UserTestRepository(ApplicationDbContext applicationDbContext, ILogger<UserTestRepository> logger)
        {
            this.applicationDbContext = applicationDbContext;
            this.logger = logger;
        }

        public async Task<List<UserTest>?> GetUserTests(string userId)
        {
            try
            {
                var userTests =  await applicationDbContext.UserTests.ToListAsync();
                return userTests;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
