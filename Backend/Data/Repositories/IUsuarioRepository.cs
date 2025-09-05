using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario? GetUsuarioById(int id);
        Usuario? GetUsuarioByEmail(string correo);
        Usuario AddUsuarioFromCredentials(string correo, string hash, byte[] salt);
        IEnumerable<Usuario> GetUsuarios(UsuarioQueryParameters query);
        void SaveChanges();
        void DeleteUsuario(int id);
    }
}
