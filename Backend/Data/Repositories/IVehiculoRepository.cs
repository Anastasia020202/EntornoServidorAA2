using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public interface IVehiculoRepository
    {
        Vehiculo? GetVehiculoById(int id);
        IEnumerable<Vehiculo> GetVehiculos();
        Vehiculo? GetVehiculoByMatricula(string matricula);
        IEnumerable<Vehiculo> GetVehiculosByUsuarioId(int usuarioId);
        Vehiculo AddVehiculo(Vehiculo vehiculo);
        void DeleteVehiculo(int id);
        void SaveChanges();
    }
}
