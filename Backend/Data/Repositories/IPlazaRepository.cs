using ParkingApp2.Models;

namespace ParkingApp2.Data.Repositories
{
    public interface IPlazaRepository
    {
        Plaza? GetPlazaById(int id);
        IEnumerable<Plaza> GetPlazas(string? tipo = null, bool? disponible = null);
        Plaza AddPlaza(Plaza plaza);
        void DeletePlaza(int id);
        void SaveChanges();
    }
}
