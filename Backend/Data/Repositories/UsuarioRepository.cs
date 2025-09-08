using Microsoft.EntityFrameworkCore;
using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ParkingDbContext _context;

        public UsuarioRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public Usuario? GetUsuarioById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario? GetUsuarioByEmail(string correo)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Correo == correo);
        }

        public Usuario AddUsuarioFromCredentials(string correo, string hash, byte[] salt)
        {
            var usuario = new Usuario
            {
                Correo = correo,
                HashContrasena = hash,
                SaltContrasena = salt,
                Rol = "User"
            };

            _context.Usuarios.Add(usuario);
            return usuario;
        }

        public IEnumerable<Usuario> GetUsuarios(UsuarioQueryParameters query)
        {
            var usuarios = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(query.Rol))
            {
                usuarios = usuarios.Where(u => u.Rol == query.Rol);
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                usuarios = usuarios.Where(u => u.Correo.Contains(query.SearchTerm));
            }

           
            usuarios = query.SortBy?.ToLower() switch
            {
                "correo" => query.Direction?.ToLower() == "desc" 
                    ? usuarios.OrderByDescending(u => u.Correo)
                    : usuarios.OrderBy(u => u.Correo),
                "rol" => query.Direction?.ToLower() == "desc" 
                    ? usuarios.OrderByDescending(u => u.Rol)
                    : usuarios.OrderBy(u => u.Rol),
                "fechacreacion" => query.Direction?.ToLower() == "desc" 
                    ? usuarios.OrderByDescending(u => u.FechaCreacion)
                    : usuarios.OrderBy(u => u.FechaCreacion),
                _ => usuarios.OrderBy(u => u.Id) 
            };

            return usuarios
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
    }
}
