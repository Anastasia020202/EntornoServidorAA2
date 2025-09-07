using Microsoft.EntityFrameworkCore;
using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public class PlazaRepository : IPlazaRepository
    {
        private readonly ParkingDbContext _context;

        public PlazaRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public Plaza? GetPlazaById(int id)
        {
            return _context.Plazas.Find(id);
        }

        public IEnumerable<Plaza> GetPlazas(string? tipo = null, bool? disponible = null)
        {
            var query = _context.Plazas.AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(p => p.Tipo.ToLower() == tipo.ToLower());
            }

            if (disponible.HasValue)
            {
                query = query.Where(p => p.Disponible == disponible.Value);
            }

            return query.ToList();
        }

        public Plaza AddPlaza(Plaza plaza)
        {
            _context.Plazas.Add(plaza);
            return plaza;
        }

        public void DeletePlaza(int id)
        {
            var plaza = _context.Plazas.Find(id);
            if (plaza != null)
            {
                _context.Plazas.Remove(plaza);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
