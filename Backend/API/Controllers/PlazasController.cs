using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;

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
        public IActionResult GetAllPlazas()
        {
            var plazas = _plazaRepository.GetPlazas();
            return Ok(plazas);
        }

        [HttpGet("{id}")]
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
        public IActionResult CreatePlaza([FromBody] CreatePlazaRequest request)
        {
            var plaza = new Plaza
            {
                Numero = request.Numero,
                Disponible = request.Disponible,
                PrecioHora = request.PrecioHora
            };

            _plazaRepository.AddPlaza(plaza);
            _plazaRepository.SaveChanges();

            return CreatedAtAction(nameof(GetPlazaById), new { id = plaza.Id }, plaza);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlaza(int id, [FromBody] UpdatePlazaRequest request)
        {
            var plaza = _plazaRepository.GetPlazaById(id);
            if (plaza == null)
            {
                return NotFound(new { message = "Plaza no encontrada" });
            }

            plaza.Numero = request.Numero;
            plaza.Disponible = request.Disponible;
            plaza.PrecioHora = request.PrecioHora;
            _plazaRepository.SaveChanges();

            return Ok(plaza);
        }

        [HttpDelete("{id}")]
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

    public class CreatePlazaRequest
    {
        public string Numero { get; set; } = "";
        public bool Disponible { get; set; } = true;
        public decimal PrecioHora { get; set; }
    }

    public class UpdatePlazaRequest
    {
        public string Numero { get; set; } = "";
        public bool Disponible { get; set; } = true;
        public decimal PrecioHora { get; set; }
    }
}
