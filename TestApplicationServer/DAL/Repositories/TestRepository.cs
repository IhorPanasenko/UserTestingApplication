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
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TestRepository> logger;

        public TestRepository(ILogger<TestRepository> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<Test?> GetById(int testId)
        {
            try
            {
                var test = await dbContext.Tests.FindAsync(testId);
                return test;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while getting data from db: {ex.Message}");
                return null;
            }
        }
    }
}
