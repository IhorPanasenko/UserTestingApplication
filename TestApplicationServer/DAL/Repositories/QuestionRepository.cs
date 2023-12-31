﻿using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ILogger<QuestionRepository> logger;
        private readonly ApplicationDbContext dbContext;

        public QuestionRepository(ILogger<QuestionRepository> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<int> CountTestQuestions(int testId)
        {
            try
            {
                var numberOfQuestoins = await dbContext.Questions.Where(q => q.TestId == testId).CountAsync();
                return numberOfQuestoins;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while getting data from db: {ex.Message}");
                return 0;
            }
        }

        public async Task<List<Question>?> GetbyTest(int testId)
        {
            try
            {
                var questions =  await dbContext.Questions.Where(q => q.TestId == testId).ToListAsync();
                return questions;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error whil egetting data from db: {ex.Message}");
                return null;
            }
        }
    }
}
