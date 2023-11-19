﻿namespace TestApplicationServer.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }

        public int Points { get; set; }

        public int TestId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public int QuestionTypeId { get; set; }

        public QuestionTypeViewModel? QuestionType { get; set; }

        public List<QuestionOptionViewModel> Options { get; set; } = new List<QuestionOptionViewModel>();
    }
}
