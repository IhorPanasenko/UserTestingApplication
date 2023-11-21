using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels.PassedTest
{
    public class UserAnswerViewModel
    {
        public string UserAnswerText { get; set; } = string.Empty;
        public int UserTestId { get; set; }
        public int QuestionId { get; set; }
        public int? UserAnswerOptionId { get; set; }
        public bool IsCorrect {  get; set; }    
    }
}