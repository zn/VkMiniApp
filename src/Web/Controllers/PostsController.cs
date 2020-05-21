using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Web.Config;
using Web.Filters;
using Web.ViewModels;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository repository;
        private readonly IMapper mapper;
        private readonly AttachmentsConfig config;
        public PostsController(IPostRepository repository, IMapper mapper, IOptions<AttachmentsConfig> config)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.config = config.Value;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await repository.GetById(id);
            return Ok(post);
        }

        [HttpGet()]
        public async Task<IActionResult> All(int page = 1)
        {
            var items = await repository.GetPostsForPage(page);
            return new JsonResult(items);
        }

        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetAuthorPosts(int id)
        {
            var posts = await repository.GetPostsByAuthor(id);
            return Ok(posts);
        }

        [HttpPost]
        [ValidateAttachments]
        public async Task<IActionResult> Post([FromForm] CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = mapper.Map<Post>(model);
                post = await repository.Create(post);
                if (model.Attachments != null)
                {
                    await repository.AddAttachments(post.Id, saveFiles(model.Attachments));
                }
                return Ok(post);
            }
            return BadRequest(model);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = mapper.Map<Post>(model);
                post = await repository.Update(post);
                return Ok(post);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await repository.GetById(id);
            await repository.Delete(post);
            return Ok();
        }

        private List<string> saveFiles(List<IFormFile> files)
        {
            List<string> paths = new List<string>(files.Count);
            List<Task> tasks = new List<Task>(files.Count);
            
            foreach (var file in files)
            {
                var task = Task.Run(() =>
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string pathToFile = Path.Combine(config.Directory, filename);
                    using (var fileStream = System.IO.File.Create(Path.Combine("wwwroot", pathToFile)))
                    {
                        file.CopyTo(fileStream);
                    }
                    paths.Add(pathToFile);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            return paths;
        }
    }
}
