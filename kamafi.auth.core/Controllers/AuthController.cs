using Microsoft.AspNetCore.Mvc;
using kamafi.auth.services;

namespace kamafi.auth.core.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(
            IAuthRepository repo)
        {
            _repo = repo;
        }
    }
}