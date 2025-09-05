using Microsoft.EntityFrameworkCore;
using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ParkingDbContext _context;

        public ReservaRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public Reserva? GetReservaById(int id)
        {
            return _context.Reservas.Find(id);
        }

        public IEnumerable<Reserva> GetReservas()
        {
            return _context.Reservas.ToList();
        }

        public IEnumerable<Reserva> GetReservasByUsuarioId(int usuarioId)
        {
            return _context.Reservas.Where(r => r.UsuarioId == usuarioId).ToList();
        }

        public IEnumerable<Reserva> GetReservasByVehiculoId(int vehiculoId)
        {
            return _context.Reservas.Where(r => r.VehiculoId == vehiculoId).ToList();
        }

        public IEnumerable<Reserva> GetReservasByPlazaId(int plazaId)
        {
            return _context.Reservas.Where(r => r.PlazaId == plazaId).ToList();
        }

        public Reserva AddReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            return reserva;
        }

        public void DeleteReserva(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
