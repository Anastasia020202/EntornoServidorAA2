namespace ParkingApp2.Models.DTOs
{
    public class UsuarioReadDto
    {
        public int Id { get; set; }
        public string Correo { get; set; } = "";
        public string Rol { get; set; } = "";
        public DateTime FechaCreacion { get; set; }
    }
}
