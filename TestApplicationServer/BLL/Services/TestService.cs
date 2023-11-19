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
        private readonly IOptionRepository optionRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IQuestionTypeRepository questionTypeRepository;

        public TestService(ILogger<TestService> logger, ITestRepository testRepository, IOptionRepository optionRepository, IQuestionRepository questionRepository, IQuestionTypeRepository questionTypeRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
            this.optionRepository = optionRepository;
            this.questionRepository = questionRepository;
            this.questionTypeRepository = questionTypeRepository;
        }

        public async Task<Test?> GetById(int testId)
        {
            try
            {
                var test = await testRepository.GetById(testId);

                if (test is null)
                {
                    throw new ArgumentException($"No Test with id {testId}");
                }

                var questions = await questionRepository.GetbyTest(test.TestId);

                if (questions is null || questions.Count == 0)
                {
                    logger.LogInformation($"No questions for test: {test.TestName} was created, or error happend");
                    return test;
                }

                foreach(var question in questions)
                {
                    var options = await optionRepository.GetByQuestion(question.QuestionId);

                    if(options is null)
                    {
                        logger.LogInformation($"For question {question.QuestionId}\t\"{question.QuestionText}\" no options created");
                        continue;
                    }

                    question.Options = options;

                    var questionType = await questionTypeRepository.GetById(question.QuestionTypeId);
                    question.QuestionType = questionType;
                }

                test.Questions = questions;
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
