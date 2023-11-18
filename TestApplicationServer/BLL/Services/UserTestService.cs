using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserTestService: IUserTestService
    {
        public readonly IUserTestRepository userTestRepository;
        public readonly ILogger<UserTestService> logger;

        public UserTestService(ILogger<UserTestService> logger, IUserTestRepository userTestRepository)
        {
            this.logger = logger;
            this.userTestRepository = userTestRepository;
        }

        public Task<List<UserTest>> GetUserTests(string userId)
        {
           throw new NotImplementedException();
        }
    }
}
