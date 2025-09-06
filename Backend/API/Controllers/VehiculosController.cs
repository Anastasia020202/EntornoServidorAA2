using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculosController(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllVehiculos()
        {
            var vehiculos = _vehiculoRepository.GetVehiculos();
            return Ok(vehiculos);
        }

        [HttpGet("{id}")]
        public IActionResult GetVehiculoById(int id)
        {
            var vehiculo = _vehiculoRepository.GetVehiculoById(id);
            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }
            return Ok(vehiculo);
        }

        [HttpGet("matricula/{matricula}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetVehiculoByMatricula(string matricula)
        {
            var vehiculo = _vehiculoRepository.GetVehiculoByMatricula(matricula);
            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }
            return Ok(vehiculo);
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetVehiculosByUsuario(int usuarioId)
        {
            var vehiculos = _vehiculoRepository.GetVehiculosByUsuarioId(usuarioId);
            return Ok(vehiculos);
        }

        [HttpPost]
        public IActionResult CreateVehiculo([FromBody] CreateVehiculoRequest request)
        {
            var vehiculo = new Vehiculo
            {
                Matricula = request.Matricula,
                Marca = request.Marca,
                Modelo = request.Modelo,
                UsuarioId = request.UsuarioId
            };

            _vehiculoRepository.AddVehiculo(vehiculo);
            _vehiculoRepository.SaveChanges();

            return CreatedAtAction(nameof(GetVehiculoById), new { id = vehiculo.Id }, vehiculo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehiculo(int id, [FromBody] UpdateVehiculoRequest request)
        {
            var vehiculo = _vehiculoRepository.GetVehiculoById(id);
            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }

            vehiculo.Matricula = request.Matricula;
            vehiculo.Marca = request.Marca;
            vehiculo.Modelo = request.Modelo;
            vehiculo.UsuarioId = request.UsuarioId;
            _vehiculoRepository.SaveChanges();

            return Ok(vehiculo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehiculo(int id)
        {
            var vehiculo = _vehiculoRepository.GetVehiculoById(id);
            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }

            _vehiculoRepository.DeleteVehiculo(id);

            return NoContent();
        }
    }

    public class CreateVehiculoRequest
    {
        public string Matricula { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int UsuarioId { get; set; }
    }

    public class UpdateVehiculoRequest
    {
        public string Matricula { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int UsuarioId { get; set; }
    }
}
