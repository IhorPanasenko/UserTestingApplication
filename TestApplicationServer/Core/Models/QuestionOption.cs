using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class QuestionOption
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        [ForeignKey("QuestionId")]
        [Required]
        public int QuestionId { get; set; }

        public Question? Question { get; set; }

        public List<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
