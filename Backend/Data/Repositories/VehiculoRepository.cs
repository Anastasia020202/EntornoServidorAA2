using Microsoft.EntityFrameworkCore;
using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly ParkingDbContext _context;

        public VehiculoRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public Vehiculo? GetVehiculoById(int id)
        {
            return _context.Vehiculos.Find(id);
        }

        public IEnumerable<Vehiculo> GetVehiculos()
        {
            return _context.Vehiculos.ToList();
        }

        public Vehiculo? GetVehiculoByMatricula(string matricula)
        {
            return _context.Vehiculos.FirstOrDefault(v => v.Matricula == matricula);
        }

        public IEnumerable<Vehiculo> GetVehiculosByUsuarioId(int usuarioId)
        {
            return _context.Vehiculos.Where(v => v.UsuarioId == usuarioId).ToList();
        }

        public Vehiculo AddVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            return vehiculo;
        }

        public void DeleteVehiculo(int id)
        {
            var vehiculo = _context.Vehiculos.Find(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
