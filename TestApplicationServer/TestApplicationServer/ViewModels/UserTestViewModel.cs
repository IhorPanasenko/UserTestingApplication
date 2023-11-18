namespace TestApplicationServer.ViewModels
{
    public class UserTestViewModel
    {
        public int UserTestId { get; set; }

        public bool IsCompleted { get; set; }

        public int? Mark { get; set; }

        public int TestId { get; set; }

        public string TestTitle { get; set; } = string.Empty;

        public int NumberOfQuestions { get; set; }

    }
}
