using Core.Models;

namespace DAL.Interfaces
{
    public interface ITestRepository
    {
        public Task<Test?> GetById(int testId);

    }
}
