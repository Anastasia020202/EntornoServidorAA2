using System.ComponentModel.DataAnnotations;

namespace ParkingApp2.Models.DTOs
{
    public class VehiculoCreateDto
    {
        [Required, MaxLength(10)]
        public string Matricula { get; set; } = "";

        [Required, MaxLength(50)]
        public string Marca { get; set; } = "";

        [Required, MaxLength(50)]
        public string Modelo { get; set; } = "";

        [MaxLength(30)]
        public string? Color { get; set; }
    }
}
