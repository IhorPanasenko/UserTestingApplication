using Core.Models;

namespace DAL.Interfaces
{
    public interface IUserTestRepository
    {
        public Task<List<UserTest>?> GetUserTests(string userId);

        public Task<bool> CreateUserTest(UserTest userTest);
    }
}
