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

        [HttpPost]
        public IActionResult CreateReserva([FromBody] CreateReservaRequest request)
        {
            var reserva = new Reserva
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                TotalAPagar = request.TotalAPagar,
                Estado = request.Estado,
                UsuarioId = request.UsuarioId,
                VehiculoId = request.VehiculoId,
                PlazaId = request.PlazaId
            };

            _reservaRepository.AddReserva(reserva);
            _reservaRepository.SaveChanges();

            return CreatedAtAction(nameof(GetReservaById), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReserva(int id, [FromBody] UpdateReservaRequest request)
        {
            var reserva = _reservaRepository.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound(new { message = "Reserva no encontrada" });
            }

            reserva.FechaInicio = request.FechaInicio;
            reserva.FechaFin = request.FechaFin;
            reserva.TotalAPagar = request.TotalAPagar;
            reserva.Estado = request.Estado;
            reserva.UsuarioId = request.UsuarioId;
            reserva.VehiculoId = request.VehiculoId;
            reserva.PlazaId = request.PlazaId;
            _reservaRepository.SaveChanges();

            return Ok(reserva);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReserva(int id)
        {
            var reserva = _reservaRepository.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound(new { message = "Reserva no encontrada" });
            }

            _reservaRepository.DeleteReserva(id);

            return NoContent();
        }
    }

    public class CreateReservaRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal TotalAPagar { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public int UsuarioId { get; set; }
        public int VehiculoId { get; set; }
        public int PlazaId { get; set; }
    }

    public class UpdateReservaRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal TotalAPagar { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public int UsuarioId { get; set; }
        public int VehiculoId { get; set; }
        public int PlazaId { get; set; }
    }
    }
}
