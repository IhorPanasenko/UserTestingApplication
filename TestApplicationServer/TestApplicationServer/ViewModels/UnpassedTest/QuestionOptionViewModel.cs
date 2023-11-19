using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestApplicationServer.ViewModels.UnpassedTest
{
    public class QuestionOptionViewModel
    {
        public int OptionId { get; set; }

        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}
