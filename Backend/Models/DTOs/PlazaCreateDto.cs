using System.ComponentModel.DataAnnotations;

namespace ParkingApp2.Models.DTOs
{
    public class PlazaCreateDto
    {
        [Required, MaxLength(10)]
        public string Numero { get; set; } = "";

        [Required, MaxLength(50)]
        public string Tipo { get; set; } = "Estándar";

        [Required]
        public decimal TarifaHora { get; set; }

        public bool Disponible { get; set; } = true;
    }
}
