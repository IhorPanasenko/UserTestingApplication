using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestService:ITestService
    {
        private readonly ILogger<TestService> logger;
        private readonly ITestRepository testRepository;

        public TestService(ILogger<TestService> logger, ITestRepository testRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
        }

        public async Task<Test?> GetById(int testId)
        {
            try
            {
                var test = await testRepository.GetById(testId);
                return test;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error in data access layer: {ex.Message}");
                return null;
            }
        }
    }
}
