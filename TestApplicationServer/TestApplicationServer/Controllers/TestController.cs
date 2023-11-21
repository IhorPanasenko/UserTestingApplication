using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApplicationServer.ViewModels.UnpassedTest;

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

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int testId, string userId)
        {
            try
            {
                var test = await testService.GetById(testId, userId);

                if(test == null)
                {
                    return BadRequest($"Info about test: {testId} is null");
                }

                var testViewModel = map(test);

                return Ok(testViewModel);
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

        private TestUnpassedViewModel map(Test test)
        {
            TestUnpassedViewModel viewModel = new TestUnpassedViewModel();
            viewModel.TestId = test.TestId;
            viewModel.TestName = test.TestName;
            viewModel.Questions = map(test.Questions);

            return viewModel;
        }

        private List<QuestionViewModel> map(List<Question> questions)
        {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();

            foreach (var question in questions) 
            {
                questionViewModels.Add(map(question));
            }

            return questionViewModels;
        }

        private QuestionViewModel map(Question question)
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            questionViewModel.QuestionText = question.QuestionText;
            questionViewModel.QuestionId = question.QuestionId;
            questionViewModel.Points = question.Points;
            questionViewModel.QuestionTypeId = question.QuestionTypeId;
            questionViewModel.QuestionType = question.QuestionType!.QuestionTypeName;
            questionViewModel.Options = map(question.Options);
            return questionViewModel;
        }

        private List<QuestionOptionViewModel> map(List<QuestionOption> options)
        {
            List<QuestionOptionViewModel> optionsViewModel = new List<QuestionOptionViewModel>();

            foreach(var option in options)
            {
                optionsViewModel.Add(map(option));
            }

            return optionsViewModel;
        }

        private QuestionOptionViewModel map(QuestionOption option)
        {
            QuestionOptionViewModel questionOptionViewModel = new QuestionOptionViewModel();
            questionOptionViewModel.OptionText = option.OptionText;
            questionOptionViewModel.QuestionId = option.QuestionId;
            questionOptionViewModel.OptionId = option.OptionId;
            questionOptionViewModel.IsCorrect = option.IsCorrect;
            return questionOptionViewModel;
        }
    }
}
