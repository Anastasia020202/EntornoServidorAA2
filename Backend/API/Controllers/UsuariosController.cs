using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsuarios()
        {
            var query = new UsuarioQueryParameters();
            var usuarios = _usuarioRepository.GetUsuarios(query);
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] CreateUsuarioRequest request)
        {
            var usuario = _usuarioRepository.AddUsuarioFromCredentials(request.Correo, request.HashContrasena, request.SaltContrasena);
            _usuarioRepository.SaveChanges();
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] UpdateUsuarioRequest request)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            usuario.Correo = request.Correo;
            usuario.Rol = request.Rol;
            _usuarioRepository.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            _usuarioRepository.DeleteUsuario(id);

            return NoContent();
        }
    }

    public class CreateUsuarioRequest
    {
        public string Correo { get; set; } = "";
        public string HashContrasena { get; set; } = "";
        public byte[] SaltContrasena { get; set; } = Array.Empty<byte>();
    }

    public class UpdateUsuarioRequest
    {
        public string Correo { get; set; } = "";
        public string Rol { get; set; } = "";
    }
}
