namespace ParkingApp2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; } = "";
        public string HashContrasena { get; set; } = "";
        public string Rol { get; set; } = "User";
    }
}
