using System.ComponentModel.DataAnnotations;

namespace ParkingApp2.Models.DTOs
{
    public class ReservaUpdateDto
    {
        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int VehiculoId { get; set; }

        [Required]
        public int PlazaId { get; set; }
    }
}
