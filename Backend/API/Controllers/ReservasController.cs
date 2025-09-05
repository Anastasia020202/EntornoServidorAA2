using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservasController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        [HttpGet]
        public IActionResult GetAllReservas()
        {
            var reservas = _reservaRepository.GetReservas();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservaById(int id)
        {
            var reserva = _reservaRepository.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound(new { message = "Reserva no encontrada" });
            }
            return Ok(reserva);
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetReservasByUsuario(int usuarioId)
        {
            var reservas = _reservaRepository.GetReservasByUsuarioId(usuarioId);
            return Ok(reservas);
        }

        [HttpGet("vehiculo/{vehiculoId}")]
        public IActionResult GetReservasByVehiculo(int vehiculoId)
        {
            var reservas = _reservaRepository.GetReservasByVehiculoId(vehiculoId);
            return Ok(reservas);
        }

        [HttpGet("plaza/{plazaId}")]
        public IActionResult GetReservasByPlaza(int plazaId)
        {
            var reservas = _reservaRepository.GetReservasByPlazaId(plazaId);
            return Ok(reservas);
        }
    }
}
