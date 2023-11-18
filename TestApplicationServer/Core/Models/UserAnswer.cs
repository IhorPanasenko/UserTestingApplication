using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        public string UserAnswerText { get; set; } = string.Empty;

        [ForeignKey("UserTestId")]
        [Required]
        public int UserTestId { get; set; }

        public UserTest? UserTest { get; set; }

        [ForeignKey("QuestionId")]
        [Required]
        public int QuestionId { get; set; }

        public Question? Question { get; set; }


        [ForeignKey("QuestionOptionId")]
        [Required]
        public int? UserAnswerOptionId { get; set; }

        public QuestionOption? QuestionOption { get; set; }
    }
}
