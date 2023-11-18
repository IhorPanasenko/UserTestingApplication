using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TestApplicationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;
        private readonly ITestService testService;

        public TestController(ILogger<TestController> logger, ITestService testService)
        {
            this.logger = logger;
            this.testService = testService;
        }

        public async Task<IActionResult> GetById(int testId)
        {
            try
            {
                var test = await testService.GetById(testId);
                return Ok(test);
            }
            catch(ArgumentException aex)
            {
                logger.LogError(aex.Message);
                return NotFound(aex.Message);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
