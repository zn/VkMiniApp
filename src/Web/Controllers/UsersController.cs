using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly ILogger<UsersController> logger;
        private readonly IMapper mapper;
        public UsersController(IUserRepository repository, IMapper mapper, ILogger<UsersController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await repository.GetUser(id);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<User>(model);
                var result = await repository.Update(user);
                bool isNewUser = result == UserUpdateResult.Created;
                if(isNewUser)
                {
                    logger.LogInformation("Added new user with VKontakte ID: " + user.VkontakteId.ToString());
                }
                return Ok(new { isNewUser });
            }
            return BadRequest(model);
        }
    }
}
