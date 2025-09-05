using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario? GetUsuarioById(int id);
    }
}
