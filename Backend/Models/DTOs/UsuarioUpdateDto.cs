using System.ComponentModel.DataAnnotations;

namespace ParkingApp2.Models.DTOs
{
    public class UsuarioUpdateDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Correo { get; set; } = "";

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; } = "";
    }
}
