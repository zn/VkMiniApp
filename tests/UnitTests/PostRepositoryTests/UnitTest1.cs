using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace PostRepositoryTests
{
    public class Tests
    {
        IPostRepository repository;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("vkminiapp_inmemory")
                .Options;
            var context = new ApplicationContext(options);
            repository = new PostRepository(context);
            addCouplePosts();
        }

        [Test]
        public async Task AddTest()
        {
            var post = new Post
            {
                Id = 132,
                AuthorVkId = 12345,
                Content = "Test123",
                PublishDate = DateTime.Now
            };
            await repository.Create(post);

            post = await repository.GetById(132);
            Assert.AreEqual(12345, post.AuthorVkId);
            Assert.AreEqual("Test123", post.Content);
        }

        [Test]
        public async Task DeleteTest()
        {
            var post = await repository.GetById(1);
            Assert.AreEqual(false, post.IsDeleted);
            await repository.Delete(post);

            post = await repository.GetById(1);
            Assert.AreEqual(true, post.IsDeleted);
        }

        [Test]
        public async Task UpdateTest()
        {
            var post = new Post
            {
                Id = 1,
                Content = "edited content"
            };
            Assert.IsFalse(post.IsEdited);
            await repository.Update(post); // need to detach created

            post = await repository.GetById(1);
            Assert.AreEqual("edited content", post.Content);
            Assert.AreEqual(true, post.IsEdited);
        }

        private void addCouplePosts()
        {
            var post1 = new Post
            {
                Id = 1,
                AuthorVkId = 12345,
                Content = "Content1",
                PublishDate = DateTime.Now
            };

            var post2 = new Post
            {
                Id = 2,
                AuthorVkId = 12345,
                Content = "Content2",
                PublishDate = DateTime.Now,
                IsDeleted = true
            };

            var post3 = new Post
            {
                Id = 3,
                AuthorVkId = 1234567,
                Content = "Content3",
                PublishDate = DateTime.Now
            };

            repository.Create(post1).Wait();
            repository.Create(post2).Wait();
            repository.Create(post3).Wait();
        }
    }
}