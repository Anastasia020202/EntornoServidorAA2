namespace ParkingApp2.Models
{
    public class UsuarioQueryParameters
    {
        public string? Rol { get; set; }
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
