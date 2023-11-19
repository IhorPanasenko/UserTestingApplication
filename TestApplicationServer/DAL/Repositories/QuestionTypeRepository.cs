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
    public class QuestionTypeRepository : IQuestionTypeRepository
    {
        private readonly ApplicationDbContext dbContext;
        private ILogger<QuestionTypeRepository> logger;

        public QuestionTypeRepository(ILogger<QuestionTypeRepository> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<QuestionType?> GetById(int id)
        {
            try
            {
                return await dbContext.QuestionTypes.FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
