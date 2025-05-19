using MottuLocation.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuLocation.Repositories
{
    public interface IMotoRepository : IGenericRepository<Moto>
    {
        Task<Moto?> GetByPlacaAsync(string placa); // Tornando anulável
        Task<Moto?> GetByRfidTagAsync(string rfidTag); // Tornando anulável
        Task<IEnumerable<Moto>> GetAllAsync(int page, int size, string sortBy, string? placaFiltro);
    }
}