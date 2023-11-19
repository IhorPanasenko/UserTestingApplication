using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels.PassingTest
{
    public class CreatingUserAnswerViewModel
    {
        public string UserAnswerText { get; set; } = string.Empty;

        [Required]
        public int UserTestId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public int? UserAnswerOptionId { get; set; }
    }
}