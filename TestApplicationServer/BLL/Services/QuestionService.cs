using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ILogger<QuestionService> logger;
        private readonly IQuestionRepository questionRepository;
        private readonly ITestRepository testRepository;

        public QuestionService(IQuestionRepository questionRepository, ILogger<QuestionService> logger, ITestRepository testRepository)
        {
            this.questionRepository = questionRepository;
            this.logger = logger;
            this.testRepository = testRepository;
        }

        public async Task<int?> CountTestMaxMark(int testId)
        {
            try
            {
                var test = testRepository.GetById(testId);

                if (test is null)
                {
                    throw new ArgumentException($"No test with Id: {testId}");
                }

                var questions = await questionRepository.GetbyTest(testId);

                if(questions is null)
                {
                    throw new ArgumentException($"No Questions for test {testId}");
                }

                var maxMark = questions.Sum(q => q.Points);
                return maxMark;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in data access layer: {ex.Message}");
                return null;
            }
        }

        public async Task<int?> CountTestQuestions(int testId)
        {
            try
            {
                var test = testRepository.GetById(testId);

                if(test is null)
                {
                    throw new ArgumentException($"No test with Id: {testId}");
                }

                var numberOfQuestions = await questionRepository.CountTestQuestions(testId);
                return numberOfQuestions;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error in data access layer: {ex.Message}");
                return null;
            }
        }
    }
}
