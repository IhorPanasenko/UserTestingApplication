namespace BLL.Interfaces
{
    public interface IQuestionService
    {
        public Task<int?> CountTestQuestions(int testId);

        public Task<int?> CountTestMaxMark(int testId); 
    }
}
