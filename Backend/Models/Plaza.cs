namespace ParkingApp2.Models
{
    public class Plaza
    {
        public int Id { get; set; }
        public string Numero { get; set; } = "";
        public string Tipo { get; set; } = ""; // Estándar, Moto, Discapacitados, Eléctrico, VIP
        public bool Disponible { get; set; } = true;
        public decimal PrecioHora { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
        public int Planta { get; set; } = 0;
        
        public List<Reserva>? Reservas { get; set; }
    }
}
