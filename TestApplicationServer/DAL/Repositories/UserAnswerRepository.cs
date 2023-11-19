using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                dbContext.Add(userAnswer);
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
                dbContext.AddRange(userAnswers);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while inserting userAnswer to db: {ex.Message}");
                return false;
            }
        }
    }
}
