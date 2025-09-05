using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;
using System.Security.Cryptography;
using System.Text;

namespace ParkingApp2.Business.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public (string hash, byte[] salt) HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (Convert.ToBase64String(hash), salt);
        }

        public Usuario Register(string correo, string password)
        {
            // Verificar si el usuario ya existe
            var existingUser = _usuarioRepository.GetUsuarioByEmail(correo);
            if (existingUser != null)
            {
                throw new InvalidOperationException("El usuario ya existe");
            }

            // Hashear la contraseña
            var (hash, salt) = HashPassword(password);

            // Crear nuevo usuario
            var usuario = _usuarioRepository.AddUsuarioFromCredentials(correo, hash, salt);
            _usuarioRepository.SaveChanges();

            return usuario;
        }

        public bool Login(string correo, string password)
        {
            // Buscar usuario por email
            var usuario = _usuarioRepository.GetUsuarioByEmail(correo);
            if (usuario == null)
            {
                return false;
            }

            // Verificar contraseña
            using var hmac = new HMACSHA256(usuario.SaltContrasena);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHash = Convert.ToBase64String(hash);

            return computedHash == usuario.HashContrasena;
        }
    }
}
