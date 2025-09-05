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
    }
}
