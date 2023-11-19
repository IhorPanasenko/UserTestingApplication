using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels
{
    public class UserAnswerViewModel
    {
        public int UserAnswerId { get; set; }

        public string UserAnswerText { get; set; } = string.Empty;

        public int UserTestId { get; set; }

        public int QuestionId { get; set; }

        public int? UserAnswerOptionId { get; set; }

        public QuestionOptionViewModel? QuestionOption { get; set; }
    }
}