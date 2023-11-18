using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class QuestionOption
    {
        [Key]
        public int OptionId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [Required]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
