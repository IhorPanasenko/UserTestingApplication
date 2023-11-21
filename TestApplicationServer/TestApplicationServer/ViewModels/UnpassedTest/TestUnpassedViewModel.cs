namespace TestApplicationServer.ViewModels.UnpassedTest
{
    public class TestUnpassedViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }
}
