using Core.Models;

namespace DAL.Interfaces
{
    public interface IUserAnswerRepository
    {
        public Task<bool> CreateUserAnswer(UserAnswer userAnswer);

        public Task<bool> CreateManyUserAnswer(List<UserAnswer> userAnswers);   
    }
}
