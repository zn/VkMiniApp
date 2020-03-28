using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository repository;
        private readonly IMapper mapper;
        public PostsController(IPostRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await repository.GetById(id);
            return Ok(post);
        }

        [HttpGet()]
        public async Task<IActionResult> All()
        {
            var items = await repository.GetAll();
            return new JsonResult(items);
        }

        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetAuthorPosts(int id)
        {
            var posts = await repository.GetPostsByAuthor(id);
            return Ok(null);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = mapper.Map<Post>(model);
                post = await repository.Create(post);
                return Ok(post);
            }
            return BadRequest(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePostViewModel model)
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
    }
}
