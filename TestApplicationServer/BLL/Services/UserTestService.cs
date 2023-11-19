﻿using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class UserTestService: IUserTestService
    {
        private readonly IUserTestRepository userTestRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserTestService> logger;

        public UserTestService(ILogger<UserTestService> logger, IUserTestRepository userTestRepository, IUserRepository userRepository)
        {
            this.logger = logger;
            this.userTestRepository = userTestRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<UserTest>?> GetUserTests(string userId)
        {
            try
            {
                var user =  await userRepository.GetById(userId);

                if (user is null)
                {
                    throw new ArgumentException($"No user with Id {userId}");
                }

                var userTests = await userTestRepository.GetUserTests(userId);
                return userTests;
            }
            catch (Exception ex)
            {
                logger.LogError($"Data access layer error:\n{ex.Message}");
                return null;
            }
        }
    }
}
