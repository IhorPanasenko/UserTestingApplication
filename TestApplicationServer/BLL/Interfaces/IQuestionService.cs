namespace BLL.Interfaces
{
    public interface IQuestionService
    {
        public Task<int?> CountTestQuestions(int testId);
    }
}
