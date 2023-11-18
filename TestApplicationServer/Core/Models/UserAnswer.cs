using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        [ForeignKey("UserTest")]
        public int UserTestId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionOption")]
        public int? UserAnswerOptionId { get; set; }

        public string UserAnswerText { get; set; } = string.Empty;
    }
}
