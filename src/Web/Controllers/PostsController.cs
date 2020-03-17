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
    [EnableCors("AllowAnyOrigin")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository repository;
        private readonly IMapper mapper;
        public PostsController(IPostRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("posts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var post = await repository.GetById(id);
                return Ok(post);
            }
            catch(PostNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("posts/author/{id}")]
        public async Task<IActionResult> GetAuthorPosts(int id)
        {
            try
            {
                var posts = await repository.GetPostsByAuthor(id);
                return Ok(posts);
            }
            catch (PostNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("posts/all")]
        public async Task<IActionResult> All()
        {
            var items = await repository.GetAll();
            return new JsonResult(items);
        }

        [HttpPost("posts/create")]
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

        [HttpPost("posts/update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var post = mapper.Map<Post>(model);
                    post = await repository.Update(post);
                    return Ok(post);
                }
                catch (PostNotFoundException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("posts/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var post = await repository.GetById(id);
                await repository.Delete(post);
                return Ok();
            }
            catch (PostNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
