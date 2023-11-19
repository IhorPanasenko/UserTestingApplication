using Core.Enums;

namespace TestApplicationServer.ViewModels.PassedTest
{
    public class PassedQuestionViewModel
    {
        public int QuestionId { get; set; }

        public int Points { get; set; }

        public int TestId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public int QuestionTypeId { get; set; }

        public EnumQuestionType QuestionType { get; set; }

        public bool isCorrect { get; set; }

        public List<PassedQuestionOptionViewModel> Options { get; set; } = new List<PassedQuestionOptionViewModel>();
    }
}
