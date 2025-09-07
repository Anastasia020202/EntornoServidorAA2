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
        [AllowAnonymous] // Zona pública - información de plazas disponible para todos
        public IActionResult GetAllPlazas()
        {
            var plazas = _plazaRepository.GetPlazas();
            return Ok(plazas);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Zona pública - información de plaza específica disponible para todos
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
