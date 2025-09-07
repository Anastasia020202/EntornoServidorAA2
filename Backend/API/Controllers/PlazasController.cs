using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;
using ParkingApp2.Models.DTOs;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlazasController : ControllerBase
    {
        private readonly IPlazaRepository _plazaRepository;

        public PlazasController(IPlazaRepository plazaRepository)
        {
            _plazaRepository = plazaRepository;
        }

        [HttpGet]
        [AllowAnonymous] // Zona pública - solo plazas disponibles para usuarios no identificados
        public IActionResult GetAllPlazas([FromQuery] string? tipo = null)
        {
            // Por defecto, solo mostrar plazas disponibles en zona pública
            var plazas = _plazaRepository.GetPlazas(tipo, disponible: true);
            return Ok(plazas);
        }

        [HttpGet("tipo/{tipo}")]
        [AllowAnonymous] // Zona pública - buscar plazas por tipo específico (discapacitados, moto, estándar, eléctrica, vip)
        public IActionResult GetPlazasByTipo(string tipo)
        {
            // Por defecto, solo mostrar plazas disponibles en zona pública
            var plazas = _plazaRepository.GetPlazas(tipo, disponible: true);
            return Ok(plazas);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")] // Solo para administradores - ver todas las plazas con filtros completos
        public IActionResult GetAllPlazasAdmin([FromQuery] string? tipo = null, [FromQuery] bool? disponible = null)
        {
            var plazas = _plazaRepository.GetPlazas(tipo, disponible);
            return Ok(plazas);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")] // Zona privada - información de plaza específica solo para usuarios autenticados
        public IActionResult GetPlazaById(int id)
        {
            var plaza = _plazaRepository.GetPlazaById(id);
            if (plaza == null)
            {
                return NotFound(new { message = "Plaza no encontrada" });
            }
            return Ok(plaza);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePlaza([FromBody] PlazaCreateDto request)
        {
            var plaza = new Plaza
            {
                Numero = request.Numero,
                Tipo = request.Tipo,
                Disponible = request.Disponible,
                PrecioHora = request.TarifaHora
            };

            _plazaRepository.AddPlaza(plaza);
            _plazaRepository.SaveChanges();

            return CreatedAtAction(nameof(GetPlazaById), new { id = plaza.Id }, plaza);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePlaza(int id, [FromBody] PlazaUpdateDto request)
        {
            var plaza = _plazaRepository.GetPlazaById(id);
            if (plaza == null)
            {
                return NotFound(new { message = "Plaza no encontrada" });
            }

            plaza.Numero = request.Numero;
            plaza.Tipo = request.Tipo;
            plaza.Disponible = request.Disponible;
            plaza.PrecioHora = request.TarifaHora;
            _plazaRepository.SaveChanges();

            return Ok(plaza);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePlaza(int id)
        {
            var plaza = _plazaRepository.GetPlazaById(id);
            if (plaza == null)
            {
                return NotFound(new { message = "Plaza no encontrada" });
            }

            _plazaRepository.DeletePlaza(id);

            return NoContent();
        }
    }
}
