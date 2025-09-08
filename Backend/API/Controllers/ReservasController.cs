using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;
using ParkingApp2.Models.DTOs;
using System.Security.Claims;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IPlazaRepository _plazaRepository;

        public ReservasController(IReservaRepository reservaRepository, IPlazaRepository plazaRepository)
        {
            _reservaRepository = reservaRepository;
            _plazaRepository = plazaRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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

            
            var reservaDto = new ReservaDto
            {
                Id = reserva.Id,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                TotalAPagar = reserva.TotalAPagar,
                Estado = reserva.Estado,
                UsuarioId = reserva.UsuarioId,
                VehiculoId = reserva.VehiculoId,
                PlazaId = reserva.PlazaId
            };

            return Ok(reservaDto);
        }

        [HttpGet("usuario/{usuarioId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetReservasByUsuario(int usuarioId)
        {
            var reservas = _reservaRepository.GetReservasByUsuarioId(usuarioId);
            return Ok(reservas);
        }

        [HttpGet("mis-reservas")]
        [Authorize(Roles = "User,Admin")] 
        public IActionResult GetMisReservas()
        {
            // Obtener el ID del usuario del token JWT
            var usuarioIdClaim = User.FindFirst("userId")?.Value ?? User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized(new { message = "Token inválido" });
            }

            var reservas = _reservaRepository.GetReservasByUsuarioId(usuarioId);
            return Ok(reservas);
        }

        [HttpGet("vehiculo/{vehiculoId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetReservasByVehiculo(int vehiculoId)
        {
            var reservas = _reservaRepository.GetReservasByVehiculoId(vehiculoId);
            return Ok(reservas);
        }

        [HttpGet("plaza/{plazaId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetReservasByPlaza(int plazaId)
        {
            var reservas = _reservaRepository.GetReservasByPlazaId(plazaId);
            return Ok(reservas);
        }

        [HttpPost]
        public IActionResult CreateReserva([FromBody] ReservaCreateDto request)
        {
           
            var plaza = _plazaRepository.GetPlazaById(request.PlazaId);
            if (plaza == null)
            {
                return BadRequest(new { message = "Plaza no encontrada" });
            }

           
            if (!plaza.Disponible)
            {
                return BadRequest(new { message = "La plaza no está disponible" });
            }

            //cnflictos horarios
            var reservasExistentes = _reservaRepository.GetReservasByPlazaId(request.PlazaId);
            foreach (var reservaExistente in reservasExistentes)
            {
                
                if (reservaExistente.Estado == "Activa" && 
                    request.FechaInicio < reservaExistente.FechaFin && 
                    request.FechaFin > reservaExistente.FechaInicio)
                {
                    return BadRequest(new { 
                        message = $"La plaza ya está reservada desde {reservaExistente.FechaInicio:HH:mm} hasta {reservaExistente.FechaFin:HH:mm}" 
                    });
                }
            }

            
            if (request.FechaFin <= request.FechaInicio)
            {
                return BadRequest(new { message = "La fecha de fin debe ser posterior a la fecha de inicio" });
            }

          
            var duracion = request.FechaFin - request.FechaInicio;
            var horas = (decimal)duracion!.Value.TotalHours;

            // Calcular el total a pagar
            var totalAPagar = plaza.PrecioHora * horas;

            var reserva = new Reserva
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                TotalAPagar = totalAPagar,
                Estado = "Activa",
                UsuarioId = request.UsuarioId,
                VehiculoId = request.VehiculoId,
                PlazaId = request.PlazaId
            };

            _reservaRepository.AddReserva(reserva);
            _reservaRepository.SaveChanges();

            
            var reservaDto = new ReservaDto
            {
                Id = reserva.Id,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                TotalAPagar = reserva.TotalAPagar,
                Estado = reserva.Estado,
                UsuarioId = reserva.UsuarioId,
                VehiculoId = reserva.VehiculoId,
                PlazaId = reserva.PlazaId
            };

            return CreatedAtAction(nameof(GetReservaById), new { id = reserva.Id }, reservaDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReserva(int id, [FromBody] ReservaUpdateDto request)
        {
            var reserva = _reservaRepository.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound(new { message = "Reserva no encontrada" });
            }

            reserva.FechaInicio = request.FechaInicio;
            reserva.FechaFin = request.FechaFin;
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
}
