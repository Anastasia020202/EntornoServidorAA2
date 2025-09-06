using ParkingApp2.Data.Repositories;
using ParkingApp2.Models;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

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

        public Usuario Register(string correo, string password, string? rol = null)
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
            
            // Asignar rol (User por defecto, Admin si se especifica)
            if (!string.IsNullOrEmpty(rol) && rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                usuario.Rol = Roles.Admin;
            }
            else
            {
                usuario.Rol = Roles.User;
            }
            
            _usuarioRepository.SaveChanges();

            return usuario;
        }


        public Usuario? Login(string correo, string password)
        {
            // Buscar usuario por email
            var usuario = _usuarioRepository.GetUsuarioByEmail(correo);
            if (usuario == null)
            {
                return null;
            }

            // Verificar contraseña
            using var hmac = new HMACSHA256(usuario.SaltContrasena);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHash = Convert.ToBase64String(hash);

            if (computedHash == usuario.HashContrasena)
            {
                return usuario;
            }

            return null;
        }

        public string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("tu-clave-secreta-super-segura-de-al-menos-32-caracteres");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = "ParkingApp2",
                Audience = "ParkingApp2",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
