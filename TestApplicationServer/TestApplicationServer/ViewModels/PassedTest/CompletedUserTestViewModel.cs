using TestApplicationServer.ViewModels.UnpassedTest;

namespace TestApplicationServer.ViewModels.PassedTest
{
    public class CompletedUserTestViewModel
    {
        public int UserTestId { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public int Mark { get; set; }
        public int MaxMark { get; set; }
        public int? NumberOfQuestions { get; set; }
        public List<PassedQuestionViewModel> Questions { get; set; } = new List<PassedQuestionViewModel>();

        /*
        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
        public List<UserAnswerViewModel> UserAnswerViewModels { get; set; } = new List<UserAnswerViewModel>();
        */
    }
}
