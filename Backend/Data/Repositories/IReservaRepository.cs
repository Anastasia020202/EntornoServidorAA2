using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public interface IReservaRepository
    {
        Reserva? GetReservaById(int id);
        IEnumerable<Reserva> GetReservas();
        IEnumerable<Reserva> GetReservasByUsuarioId(int usuarioId);
        IEnumerable<Reserva> GetReservasByVehiculoId(int vehiculoId);
        IEnumerable<Reserva> GetReservasByPlazaId(int plazaId);
        Reserva AddReserva(Reserva reserva);
        void DeleteReserva(int id);
        void SaveChanges();
    }
}
