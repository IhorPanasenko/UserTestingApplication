namespace TestApplicationServer.ViewModels
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
        public UserTestViewModel? UserTest { get; set; }
    }
}
