using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels.PassingTest
{
    public class PassingUserTestViewModel
    {
        [Required]
        public int UserTestId { get; set; } 

        [Required]
        public string AppUserId { get; set; } = string.Empty;

        [Required]
        public int TestId { get; set; }

        [Required]
        public List<CreatingUserAnswerViewModel> UserAnswers { get; set; } = new List<CreatingUserAnswerViewModel>();
    }
}
