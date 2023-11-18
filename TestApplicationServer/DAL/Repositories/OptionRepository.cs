using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    internal class OptionRepository : IOptionRepository
    {
        private readonly ILogger<OptionRepository> logger;
        private readonly ApplicationDbContext dbContext;
        public OptionRepository(ILogger<OptionRepository> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<List<QuestionOption>?> GetByQuestion(int questionId)
        {
            try
            {
                var options = await dbContext.QuestionOption.Where(qo => qo.QuestionId == questionId).ToListAsync();
                return options;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while getting data from db {ex.Message}");
                return null;
            }
        }
    }
}
