namespace ParkingApp2.Models
{
    public class Plaza
    {
        public int Id { get; set; }
        public string Numero { get; set; } = "";
        public bool Disponible { get; set; } = true;
        public decimal PrecioHora { get; set; }
    }
}
