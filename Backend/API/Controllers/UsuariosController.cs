using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsuarios([FromQuery] string? rol = null, [FromQuery] string? searchTerm = null, 
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10, 
            [FromQuery] string? sortBy = null, [FromQuery] string? direction = "asc")
        {
            var query = new UsuarioQueryParameters
            {
                Rol = rol,
                SearchTerm = searchTerm,
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                Direction = direction
            };
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


    public class UpdateUsuarioRequest
    {
        public string Correo { get; set; } = "";
        public string Rol { get; set; } = "";
    }
}
