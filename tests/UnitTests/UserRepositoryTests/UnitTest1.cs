using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UserRepositoryTests
{
    public class Tests
    {
        IUserRepository repository;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("vkminiapp_inmemory")
                .Options;
            var context = new ApplicationContext(options);
            repository = new UserRepository(context);
        }

        [Test]
        public async Task CreateUpdateUserTest()
        {
            User user = createUser();

            UserUpdateResult result = await repository.Update(user);
            Assert.AreEqual(UserUpdateResult.Created, result);

            user.FirstName = "Joe";
            result = await repository.Update(user);
            Assert.AreEqual(UserUpdateResult.Updated, result);
        }

        [Test]
        public async Task GetUserTest()
        {
            User user = createUser();
            UserUpdateResult result = await repository.Update(user);
            Assert.AreEqual(UserUpdateResult.Created, result);

            user = await repository.GetUser(user.VkontakteId);
            Assert.AreEqual("Doe", user.LastName);

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.GetUser(-123));
        }

        private User createUser()
        {
            return new User
            {
                VkontakteId = 12345,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Parse("10.2.2000"),
                Sex = false,
                Photo100 = "url",
                Photo200 = "url"
            };
        }
    }
}