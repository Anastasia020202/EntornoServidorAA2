namespace ParkingApp2.Models
{
    public class Plaza
    {
        public int Id { get; set; }
        public string Numero { get; set; } = "";
        public string Tipo { get; set; } = ""; // Estándar, Moto, Discapacitados, Eléctrico, VIP
        public bool Disponible { get; set; } = true;
        public decimal PrecioHora { get; set; }
        
        public List<Reserva>? Reservas { get; set; }
    }
}
