using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
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

        public UserTestsController(ILogger<UserTestsController> logger, IUserTestService userTestService, IQuestionService questionService)
        {
            this.logger = logger;
            this.userTestService = userTestService;
            this.questionService = questionService;
        }

        [HttpGet("GetUserTests")]
        public async Task<IActionResult> GetUserTests(string userId)
        {
            try
            {
                var userTests = await userTestService.GetUserTests(userId);

                if(userTests == null)
                {
                    return NotFound($"Can't find tests for user {userId}");
                }

                var userTestsViews = map(userTests);

                foreach( var view in userTestsViews)
                {
                    var questionNumber =  await questionService.CountTestQuestions(view.TestId);
                    view.NumberOfQuestions = questionNumber;
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

        [HttpPut("PassTheTest")]
        public async Task<IActionResult> PassTheTest(PassingUserTestViewModel passingUserTest)
        {
            var userTest = map(passingUserTest);

            try
            {
                var res = await userTestService.CreateUserTest(userTest);

                if (res is false)
                {
                    return BadRequest("Can't create new UserTest");
                }

                return Ok("User's answers were saved");
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
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

            foreach(var createAnswer in creatingUserAnswers)
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

            foreach(var userTest in userTests)
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
                TestTitle = userTest.Test.TestName
            };
        }

    }
}
