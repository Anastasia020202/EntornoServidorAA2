using System.ComponentModel.DataAnnotations;

namespace ParkingApp2.Models.DTOs
{
    public class UsuarioCreateDto
    {
        [Required, EmailAddress]
        public string Correo { get; set; } = "";

        [Required, MinLength(6)]
        public string Contrasena { get; set; } = "";

        public string? Rol { get; set; }
    }
}