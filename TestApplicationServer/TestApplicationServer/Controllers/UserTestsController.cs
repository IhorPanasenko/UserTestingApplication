using BLL.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApplicationServer.ViewModels.PassedTest;
using TestApplicationServer.ViewModels.PassingTest;
using TestApplicationServer.ViewModels.TestInfo;

namespace TestApplicationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestsController : ControllerBase
    {
        private readonly ILogger<UserTestsController> logger;
        private readonly IUserTestService userTestService;
        private readonly IQuestionService questionService;
        private readonly IUserAnswersService userAnswersService;
        private readonly ITestService testService;
        private readonly IQuestionTypeService questionTypeService;
        private readonly IOptionService optionService;

        public UserTestsController(
            ILogger<UserTestsController> logger,
            IUserTestService userTestService,
            IQuestionService questionService,
            IUserAnswersService userAnswersService,
            ITestService testService,
            IQuestionTypeService questionTypeService,
            IOptionService optionService)
        {
            this.logger = logger;
            this.userTestService = userTestService;
            this.questionService = questionService;
            this.userAnswersService = userAnswersService;
            this.testService = testService;
            this.questionTypeService = questionTypeService;
            this.optionService = optionService;
        }

        [HttpGet("GetUserTests")]
        [Authorize]
        public async Task<IActionResult> GetUserTests(string userId)
        {
            try
            {
                var userTests = await userTestService.GetUserTests(userId);

                if (userTests == null)
                {
                    return NotFound($"Can't find tests for user {userId}");
                }

                var userTestsViews = map(userTests);

                foreach (var view in userTestsViews)
                {
                    var questionNumber = await questionService.CountTestQuestions(view.TestId);
                    view.NumberOfQuestions = questionNumber;

                    var maxMark = await questionService.CountTestMaxMark(view.TestId);
                    view.MaxMark = maxMark ?? 0;
                }

                return Ok(userTestsViews);
            }
            catch (ArgumentException aex)
            {
                logger.LogError(aex.Message);
                return NotFound(aex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCompletedTest")]
        [Authorize]
        public async Task<IActionResult> GetCompletedTest(int userTestId)
        {
            try
            {
                var userTest = await userTestService.GetById(userTestId);

                if (userTest == null)
                {
                    return NotFound($"Can't find data for user test {userTestId}");
                }

                var userAnswers = await userAnswersService.GetByUSerTest(userTestId);

                if (userAnswers is null)
                {
                    return NotFound($"Can't find user answers for user test {userTestId}");
                }

                userTest.UserAnswers = userAnswers;
                var test = await testService.GetById(userTest.TestId, userTest.AppUserId);

                if (test is null)
                {
                    return NotFound($"Can't find test with id {userTest.TestId}");
                }

                userTest.Test = test;
                CompletedUserTestViewModel userTestViewModel = await convert(userTest);

                userTestViewModel.NumberOfQuestions = await questionService.CountTestQuestions(userTestViewModel.TestId);
                userTestViewModel.MaxMark = await questionService.CountTestMaxMark(userTestViewModel.TestId) ?? 0;
                return Ok(userTestViewModel);

            }
            catch (Exception ex)
            {
                logger.LogError("ex.Message");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PassTheTest")]
        [Authorize]
        public async Task<IActionResult> PassTheTest(PassingUserTestViewModel passingUserTest)
        {
            var userTest = map(passingUserTest);

            try
            {
                var res = await userTestService.PassUserTest(userTest);

                if (res is false)
                {
                    return BadRequest($"Can't pass test for user {passingUserTest.AppUserId}");
                }

                return Ok("User's answers were saved");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private async Task<CompletedUserTestViewModel> convert(UserTest userTest)
        {
            CompletedUserTestViewModel completedUserTest = new CompletedUserTestViewModel();
            completedUserTest.UserTestId = userTest.UserTestId;
            completedUserTest.TestId = userTest.TestId;
            completedUserTest.TestName = userTest.Test!.TestName;
            completedUserTest.Mark = userTest.Mark ?? 0;
            completedUserTest.Questions = await map(userTest.Test.Questions, userTest.UserAnswers);

            return completedUserTest;
        }

        private async Task<List<PassedQuestionViewModel>> map(List<Question> questions, List<UserAnswer> userAnswers)
        {
            List<PassedQuestionViewModel> passedQuestionViewModels = new List<PassedQuestionViewModel>();

            questions = questions.OrderBy(q => q.QuestionId).ToList();
            userAnswers = userAnswers.OrderBy(ua => ua.QuestionId).ToList();

            foreach (var currentQuestion in questions)
            {
                currentQuestion.QuestionType = await questionTypeService.GetById(currentQuestion.QuestionTypeId);

                PassedQuestionViewModel passedQuestionViewModel = new PassedQuestionViewModel();
                passedQuestionViewModel.QuestionId = currentQuestion.QuestionId;
                passedQuestionViewModel.TestId = currentQuestion.TestId;
                passedQuestionViewModel.QuestionText = currentQuestion.QuestionText;
                passedQuestionViewModel.Points = currentQuestion.Points;
                passedQuestionViewModel.QuestionTypeId = currentQuestion.QuestionTypeId;
                passedQuestionViewModel.QuestionType = currentQuestion.QuestionType!.QuestionTypeName;

                var questionType = currentQuestion.QuestionType;
                var options = await optionService.GetByQuestion(currentQuestion.QuestionId);

                if (options == null)
                {
                    throw new ArgumentException($"Can't find options for question {currentQuestion.QuestionId}");
                }

                var currentAnswers = userAnswers.Where(ua => ua.QuestionId == currentQuestion.QuestionId).ToList();
                List<PassedQuestionOptionViewModel> passedQuestionOptionViewModels = new List<PassedQuestionOptionViewModel>();

                switch (questionType!.QuestionTypeName)
                {
                    case EnumQuestionType.SingleAnswer:
                        passedQuestionViewModel.IsCorrectAnswered = currentAnswers[0].IsCorrect;

                        foreach (var option in options)
                        {
                            PassedQuestionOptionViewModel passedQuestionOptionViewModel = new PassedQuestionOptionViewModel();
                            passedQuestionOptionViewModel.OptionId = option.OptionId;
                            passedQuestionOptionViewModel.QuestionId = option.QuestionId;
                            passedQuestionOptionViewModel.IsCorrect = option.IsCorrect;
                            passedQuestionOptionViewModel.OptionText = option.OptionText;
                            passedQuestionOptionViewModel.IsChoosen = option.OptionId == currentAnswers[0].UserAnswerOptionId;
                            passedQuestionOptionViewModels.Add(passedQuestionOptionViewModel);
                        }

                        break;
                    case EnumQuestionType.MultipleAnswers:
                        bool isAllCorrect = true;

                        foreach (var answer in currentAnswers)
                        {
                            if (!answer.IsCorrect)
                            {
                                isAllCorrect = false;
                            }
                        }

                        passedQuestionViewModel.IsCorrectAnswered = isAllCorrect;

                        foreach (var option in options)
                        {
                            PassedQuestionOptionViewModel passedQuestionOptionViewModel = new PassedQuestionOptionViewModel();
                            passedQuestionOptionViewModel.OptionId = option.OptionId;
                            passedQuestionOptionViewModel.QuestionId = option.QuestionId;
                            passedQuestionOptionViewModel.IsCorrect = option.IsCorrect;
                            passedQuestionOptionViewModel.OptionText = option.OptionText;
                            var isAnswerChosen = currentAnswers.FirstOrDefault(ca => ca.UserAnswerOptionId == option.OptionId);
                            passedQuestionOptionViewModel.IsChoosen = isAnswerChosen != null;

                            passedQuestionOptionViewModels.Add(passedQuestionOptionViewModel);
                        }
                        break;

                    case EnumQuestionType.OpenQuestion:
                        passedQuestionViewModel.IsCorrectAnswered = currentAnswers[0].IsCorrect;

                        foreach (var option in options)
                        {
                            PassedQuestionOptionViewModel passedQuestionOptionViewModel = new PassedQuestionOptionViewModel();
                            passedQuestionOptionViewModel.OptionId = option.OptionId;
                            passedQuestionOptionViewModel.QuestionId = option.QuestionId;
                            passedQuestionOptionViewModel.IsCorrect = option.IsCorrect;
                            passedQuestionOptionViewModel.OptionText = option.OptionText;
                            passedQuestionOptionViewModel.EnteredText = currentAnswers[0].UserAnswerText;
                            var isAnswerChosen = currentAnswers.FirstOrDefault(ca => ca.UserAnswerOptionId == option.OptionId);
                            passedQuestionOptionViewModel.IsChoosen = isAnswerChosen == null;

                            passedQuestionOptionViewModels.Add(passedQuestionOptionViewModel);
                        }
                        break;
                    default:
                        throw new ArgumentException("Unknown question type");
                }

                passedQuestionViewModel.Options = passedQuestionOptionViewModels;
                passedQuestionViewModels.Add(passedQuestionViewModel);
            }

            return passedQuestionViewModels;
        }

        private UserTest map(PassingUserTestViewModel passingUserTest)
        {
            UserTest userTest = new UserTest();
            userTest.UserTestId = passingUserTest.UserTestId;
            userTest.TestId = passingUserTest.TestId;
            userTest.AppUserId = passingUserTest.AppUserId;
            userTest.IsCompleted = true;
            userTest.UserAnswers = map(passingUserTest.UserAnswers);
            return userTest;
        }

        private List<UserAnswer> map(List<CreatingUserAnswerViewModel> creatingUserAnswers)
        {
            List<UserAnswer> userAnswers = new List<UserAnswer>();

            foreach (var createAnswer in creatingUserAnswers)
            {
                userAnswers.Add(map(createAnswer));
            }

            return userAnswers;
        }

        private UserAnswer map(CreatingUserAnswerViewModel createAnswer)
        {
            UserAnswer userAnswer = new UserAnswer();
            userAnswer.UserAnswerText = createAnswer.UserAnswerText;
            userAnswer.UserTestId = createAnswer.UserTestId;
            userAnswer.UserAnswerOptionId = createAnswer.UserAnswerOptionId;
            userAnswer.QuestionId = createAnswer.QuestionId;
            return userAnswer;
        }

        private List<UserTestInfoViewModel> map(List<UserTest> userTests)
        {
            List<UserTestInfoViewModel> res = new List<UserTestInfoViewModel>();

            foreach (var userTest in userTests)
            {
                res.Add(map(userTest));
            }

            return res;
        }

        private UserTestInfoViewModel map(UserTest userTest)
        {
            return new UserTestInfoViewModel
            {
                UserTestId = userTest.UserTestId,
                TestId = userTest.TestId,
                IsCompleted = userTest.IsCompleted,
                Mark = userTest.Mark,
                TestTitle = userTest.Test!.TestName,
            };
        }

    }
}
