namespace ParkingApp2.Models.DTOs
{
    public class VehiculoDto
    {
        public int Id { get; set; }
        public string Matricula { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public string? Color { get; set; }
        public int UsuarioId { get; set; }
    }
}
