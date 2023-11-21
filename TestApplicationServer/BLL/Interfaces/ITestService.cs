using Core.Models;

namespace BLL.Interfaces
{
    public interface ITestService
    {
        public Task<Test?> GetById(int testId, string userId);
    }
}
