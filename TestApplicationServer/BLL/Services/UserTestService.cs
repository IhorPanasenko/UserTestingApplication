using BLL.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class UserTestService : IUserTestService
    {
        private readonly IUserTestRepository userTestRepository;
        private readonly IUserRepository userRepository;
        private readonly ITestRepository testRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IQuestionTypeRepository questionTypeRepository;
        private readonly IOptionRepository optionRepository;
        private readonly IUserAnswerRepository userAnswerRepository;
        private readonly ILogger<UserTestService> logger;

        public UserTestService(
            ILogger<UserTestService> logger, 
            IUserTestRepository userTestRepository, 
            IUserRepository userRepository, 
            ITestRepository testRepository, 
            IQuestionRepository questionRepository, 
            IQuestionTypeRepository questionTypeRepository, 
            IOptionRepository optionRepository, 
            IUserAnswerRepository userAnswerRepository
            )
        {
            this.logger = logger;
            this.userTestRepository = userTestRepository;
            this.userRepository = userRepository;
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
            this.questionTypeRepository = questionTypeRepository;
            this.optionRepository = optionRepository;
            this.userAnswerRepository = userAnswerRepository;
        }

        public async Task<bool> CreateUserTest(UserTest userTest)
        {
            try
            {
                var isTestAssigned = await userTestRepository.GetById(userTest.UserTestId);

                if(isTestAssigned == null || isTestAssigned.AppUserId == userTest.AppUserId)
                {
                    throw new ArgumentException("You are trying to pass unexisting test assignment");
                }

                if (isTestAssigned.IsCompleted)
                {
                    throw new ArgumentException("You are trying to pass akready completed test");
                }

                int mark = await computeMark(userTest);
                userTest.Mark = mark;
                var userAnswers = userTest.UserAnswers;
                var res = await userAnswerRepository.CreateManyUserAnswer(userAnswers);

                if(res == false)
                {
                    logger.LogError("Can't save user's answers");
                    return res;
                }

                res = await userTestRepository.CreateUserTest(userTest);

                if(res == false)
                {
                    logger.LogError("Can't save user test");
                }

                return res;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        private async Task<int> computeMark(UserTest userTest)
        {
            int mark = 0;
            var questionsInTest = await questionRepository.GetbyTest(userTest.TestId);

            if (questionsInTest == null)
            {
                throw new ArgumentException($"For test {userTest.TestId} can't get questions");
            }

            var answersByUser = userTest.UserAnswers.OrderBy(a => a.QuestionId).ToList();
            questionsInTest = questionsInTest.OrderBy(q => q.QuestionId).ToList();

            foreach(var question in questionsInTest)
            {
                question.QuestionType = await questionTypeRepository.GetById(question.QuestionTypeId);
            }
          
            for (int i = 0; i < answersByUser.Count(); ++i)
            {
                var currentQuestion = questionsInTest.First(q => q.QuestionId == answersByUser[i].QuestionId);
                var questionType = currentQuestion.QuestionType;
                var options = await optionRepository.GetByQuestion(currentQuestion.QuestionId);

                if (options == null)
                {
                    throw new ArgumentException($"Can't find options for question {currentQuestion.QuestionId}");
                }

                switch (questionType!.QuestionTypeName)
                {
                    case EnumQuestionType.SingleAnswer:
                        var isCorrect = options.Find(o => o.OptionId == answersByUser[i].UserAnswerOptionId)!.IsCorrect;

                        if (isCorrect)
                        {
                            mark += currentQuestion.Points;
                        }

                        break;
                    case EnumQuestionType.MultipleAnswers:
                        bool allCorrect = true;

                        while(currentQuestion.QuestionId == answersByUser[i].QuestionId)
                        {
                            isCorrect = options.Find(o => o.OptionId == answersByUser[i].UserAnswerOptionId)!.IsCorrect;

                            if (!isCorrect)
                            {
                                allCorrect = false;
                            }
                           
                            i++;
                        }

                        if (allCorrect)
                        {
                            mark += currentQuestion.Points;
                        }
                        
                        break;
                    case EnumQuestionType.OpenQuestion:
                        var correctOption = options.Find(o => o.OptionId == answersByUser[i].UserAnswerOptionId)!;

                        if (answersByUser[i].UserAnswerText == correctOption.OptionText)
                        {
                            mark += currentQuestion.Points;
                        }

                        break;
                    default:
                        throw new ArgumentException("Unknown question type");
                }
                
            }

            return mark;
        }

        public async Task<List<UserTest>?> GetUserTests(string userId)
        {
            try
            {
                var user = await userRepository.GetById(userId);

                if (user is null)
                {
                    throw new ArgumentException($"No user with Id {userId}");
                }

                var userTests = await userTestRepository.GetUserTests(userId);

                if (userTests is null)
                {
                    logger.LogError($"Test for user {userId} returned null");
                    return userTests;
                }

                foreach (var ut in userTests)
                {
                    ut.Test = await testRepository.GetById(ut.TestId);
                }

                return userTests;
            }
            catch (Exception ex)
            {
                logger.LogError($"Data access layer error:\n{ex.Message}");
                return null;
            }
        }
    }
}
