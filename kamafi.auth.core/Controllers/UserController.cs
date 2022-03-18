using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using kamafi.auth.data.exceptions;
using kamafi.auth.data.models;
using kamafi.auth.services;

namespace kamafi.auth.core.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _repo.GetAsync());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserAsync([FromRoute, Required] int userId)
        {
            return Ok(await _repo.GetAsync(userId));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody] UserDto dto)
        {
            return Ok(await _repo.AddAsync(dto));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequest request)
        {
            return Ok(await _repo.GetTokenAsync(request));
        }
    }
}
