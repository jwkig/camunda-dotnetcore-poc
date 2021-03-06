using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sso.Domain;

namespace Sso.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthService authService;

        public UserController(AuthService authService)
        {
            this.authService = authService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest user)
        {
            var authResult = authService.Authenticate(user.Login, user.Password);

            if (authResult == null)
                return BadRequest(new {message = "Username of password incorrect"});

            return Ok(authResult);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(authService.UserWithLogin(HttpContext.User.Identity.Name));
        }
    }
}