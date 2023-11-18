using Microsoft.AspNetCore.Mvc;

namespace TestApplicationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;

        public TestController(ILogger<TestController> logger)
        {
            this.logger = logger;
        }

        public Task<IActionResult> GetById(int testId)
        {
            throw new NotImplementedException();
        }
    }
}
