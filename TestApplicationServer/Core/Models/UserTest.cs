using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class UserTest
    {
        [Key]
        public int UserTestId { get; set; }

        public bool IsCompleted { get; set; }

        public int? Mark { get; set; }

        [ForeignKey("AppUserId")]
        [Required]
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }

        [ForeignKey("TestId")]
        [Required]
        public int TestId { get; set; }

        public Test? Test { get; set; }

        public List<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
