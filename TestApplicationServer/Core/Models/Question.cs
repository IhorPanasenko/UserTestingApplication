using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public int Points { get; set; }

        [ForeignKey("TestId")]
        [Required]
        public int TestId { get; set; }

        public Test? Test { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        [ForeignKey("QuestionTypeId")]
        [Required]
        public int QuestionTypeId { get; set; }

        public QuestionType? QuestionType { get; set; }

        public List<QuestionOption> Options { get; set; } = new List<QuestionOption>();

        public List<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    
    }
}
