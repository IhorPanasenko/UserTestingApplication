using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels
{
    public class PassingUserTestViewModel
    {
        [Required]
        public string AppUserId { get; set; } = string.Empty;

        [Required]
        public int TestId { get; set; }

        [Required]
        public List<UserAnswerViewModel> UserAnswers { get; set; } = new List<UserAnswerViewModel>();
    }
}
