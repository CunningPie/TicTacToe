using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<string>> Get()
        {
            return await _userRepository.GetUserNames();
        }
        
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<Guid> SignIn(string username, string password)
        {
            var userId = await _userRepository.Authenticate(username, password);
            if (userId != Guid.Empty)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 401;
            }

            return userId;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<Guid> SignUp(string username, string password)
        {
            var userId = await _userRepository.SignUp(username, password);

            if (userId != Guid.Empty)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 401;
            }

            return userId;
        }
    }
}
