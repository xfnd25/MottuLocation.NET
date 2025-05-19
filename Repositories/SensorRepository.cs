using Microsoft.EntityFrameworkCore;
using MottuLocation.Data;
using MottuLocation.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuLocation.Repositories
{
    public class SensorRepository : GenericRepository<Sensor>, ISensorRepository
    {
        private readonly new MottuLocationDbContext _context;

        public SensorRepository(MottuLocationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Sensor?> GetByCodigoAsync(string codigo) // Tornando o tipo de retorno anulável
        {
            return await _context.Sensores.FirstOrDefaultAsync(s => s.Codigo == codigo);
        }

        public async Task<IEnumerable<Sensor>> GetAllAsync(int page, int size, string sortBy, string? codigoFiltro)
        {
            var query = _context.Sensores.AsQueryable();

            if (!string.IsNullOrEmpty(codigoFiltro))
            {
                query = query.Where(s => s.Codigo.Contains(codigoFiltro));
            }

            switch (sortBy.ToLower())
            {
                case "id":
                    query = query.OrderBy(s => s.Id);
                    break;
                case "codigo":
                    query = query.OrderBy(s => s.Codigo);
                    break;
                default:
                    query = query.OrderBy(s => s.Codigo); // Default sort
                    break;
            }

            return await query.Skip(page * size).Take(size).ToListAsync();
        }
    }
}