using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Business.Services;
using ParkingApp2.Models.DTOs;

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
        public IActionResult Register([FromBody] UsuarioCreateDto request)
        {
            try
            {
                var usuario = _authService.Register(request.Correo, request.Contrasena);
                return Ok(new { message = "Usuario registrado correctamente", usuarioId = usuario.Id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDto request)
        {
            var usuario = _authService.Login(request.Correo, request.Contrasena);
            if (usuario != null)
            {
                var token = _authService.GenerateJwtToken(usuario);
                return Ok(new { 
                    message = "Login exitoso", 
                    token = token,
                    usuario = new { 
                        id = usuario.Id, 
                        correo = usuario.Correo, 
                        rol = usuario.Rol 
                    }
                });
            }
            return Unauthorized(new { message = "Credenciales inválidas" });
        }


    }

}
