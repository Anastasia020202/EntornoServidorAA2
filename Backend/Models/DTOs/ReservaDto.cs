namespace ParkingApp2.Models.DTOs
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal TotalAPagar { get; set; }
        public string Estado { get; set; } = "";
        public int UsuarioId { get; set; }
        public int VehiculoId { get; set; }
        public int PlazaId { get; set; }
    }
}
