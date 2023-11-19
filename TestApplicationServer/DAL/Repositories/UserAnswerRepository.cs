using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ILogger<UserAnswerRepository> logger;
        private readonly ApplicationDbContext dbContext;

        public UserAnswerRepository(ILogger<UserAnswerRepository> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateUserAnswer(UserAnswer userAnswer)
        {
            try
            {
                dbContext.UserAnswers.Add(userAnswer);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while inserting userAnswer to db: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateManyUserAnswer(List<UserAnswer> userAnswers)
        {
            try
            {
                dbContext.UserAnswers.AddRange(userAnswers);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while inserting userAnswer to db: {ex.Message}");
                return false;
            }
        }

        public async Task<List<UserAnswer>?> GetForTest(int userTestId)
        {
            try
            {
                return await dbContext.UserAnswers.Where(ua => ua.UserTestId == userTestId).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
