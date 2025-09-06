using System.ComponentModel.DataAnnotations;

namespace ParkingApp2.Models.DTOs
{
    public class UsuarioLoginDto
    {
        [Required, EmailAddress]
        public string Correo { get; set; } = "";

        [Required]
        public string Contrasena { get; set; } = "";
    }
}
