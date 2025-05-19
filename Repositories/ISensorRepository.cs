using MottuLocation.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuLocation.Repositories
{
    public interface ISensorRepository : IGenericRepository<Sensor>
    {
        Task<Sensor?> GetByCodigoAsync(string codigo); // Tornando anulável
        Task<IEnumerable<Sensor>> GetAllAsync(int page, int size, string sortBy, string? codigoFiltro);
    }
}