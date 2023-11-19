namespace TestApplicationServer.ViewModels.PassedTest
{
    public class PassedQuestionOptionViewModel
    {
        public int OptionId { get; set; }

        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public bool IsChoosen { get; set; }
    }
}
