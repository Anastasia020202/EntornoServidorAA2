using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Business.Services;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                var usuario = _authService.Register(request.Correo, request.Password);
                return Ok(new { message = "Usuario registrado correctamente", usuarioId = usuario.Id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var isValid = _authService.Login(request.Correo, request.Password);
            if (isValid)
            {
                return Ok(new { message = "Login exitoso" });
            }
            return Unauthorized(new { message = "Credenciales inválidas" });
        }
    }

    public class RegisterRequest
    {
        public string Correo { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginRequest
    {
        public string Correo { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
