using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Web.ViewModels;

[assembly:InternalsVisibleTo("UserControllerTests")]

namespace Web.Controllers
{
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        public UserController(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await repository.GetUser(id);
                return Ok(user);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("users/update")]
        public async Task<IActionResult> UpdateInfo(UpdateUserInfoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = mapper.Map<User>(model);
                    var result = await repository.Update(user);
                    return Ok(new { newUser = result == UserUpdateResult.Created });
                }
                ModelState.AddModelError("", Thread.CurrentThread.CurrentCulture.DisplayName);
                return BadRequest(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
