namespace ParkingApp2.Models.DTOs
{
    public class PlazaDto
    {
        public int Id { get; set; }
        public string Numero { get; set; } = "";
        public string Zona { get; set; } = "";
        public bool Disponible { get; set; }
        public decimal TarifaHora { get; set; }
    }
}
