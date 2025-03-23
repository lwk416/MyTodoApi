using Microsoft.AspNetCore.Mvc;
using MyToDoApi.Services;

namespace MyToDoApi.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService; 
        
        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == "admin" && model.Password == "admin")
            {
                var token = _jwtService.GenerateToken(model.Username);
                return Ok(new { token });
            }

            return Unauthorized("Invalid credentails");
        }

        public record LoginModel(string Username, string Password); 
    }
}
