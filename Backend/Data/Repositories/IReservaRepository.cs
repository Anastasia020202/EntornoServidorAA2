using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public interface IReservaRepository
    {
        Reserva? GetReservaById(int id);
        IEnumerable<Reserva> GetReservas();
        IEnumerable<Reserva> GetReservasByUsuarioId(int usuarioId);
    }
}
