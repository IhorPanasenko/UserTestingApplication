using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplicationServer.ViewModels;

namespace TestApplicationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestsController : ControllerBase
    {
        private readonly ILogger<UserTestsController> logger;
        private readonly IUserTestService userTestService;

        public UserTestsController(ILogger<UserTestsController> logger, IUserTestService userTestService)
        {
            this.logger = logger;
            this.userTestService = userTestService;
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


                return Ok(userTestsViews);
            }
            catch(ArgumentException aex)
            {
                logger.LogError(aex.Message);
                return NotFound($"No User with given Id: {userId}");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private List<UserTestViewModel> map(List<UserTest> userTests)
        {
            List<UserTestViewModel> res = new List<UserTestViewModel>();

            foreach(var userTest in userTests)
            {
                res.Add(map(userTest));
            }

            return res;
        }

        private UserTestViewModel map(UserTest userTest)
        {
            return new UserTestViewModel
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
