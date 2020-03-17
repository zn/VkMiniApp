using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Web.Controllers;

namespace UserControllerTests
{
    public class Tests
    {
        UserController controller;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("vkminiapp_inmemory")
                .Options;
            var context = new ApplicationContext(options);
            var repository = new UserRepository(context);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}