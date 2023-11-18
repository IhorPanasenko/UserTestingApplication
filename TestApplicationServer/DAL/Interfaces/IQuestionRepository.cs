namespace DAL.Interfaces
{
    public interface IQuestionRepository
    {
        public Task<int> CountTestQuestions(int testId);
    }
}
