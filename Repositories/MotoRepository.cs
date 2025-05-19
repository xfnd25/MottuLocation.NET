using Microsoft.EntityFrameworkCore;
using MottuLocation.Data;
using MottuLocation.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuLocation.Repositories
{
    public class MotoRepository : GenericRepository<Moto>, IMotoRepository
    {
        private readonly new MottuLocationDbContext _context;

        public MotoRepository(MottuLocationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Moto?> GetByPlacaAsync(string placa) // Tornando o tipo de retorno anulável
        {
            return await _context.Motos.FirstOrDefaultAsync(m => m.Placa == placa);
        }

        public async Task<Moto?> GetByRfidTagAsync(string rfidTag) // Tornando o tipo de retorno anulável
        {
            return await _context.Motos.FirstOrDefaultAsync(m => m.RfidTag == rfidTag);
        }

        public async Task<IEnumerable<Moto>> GetAllAsync(int page, int size, string sortBy, string? placaFiltro)
        {
            var query = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(placaFiltro))
            {
                query = query.Where(m => m.Placa.Contains(placaFiltro));
            }

            switch (sortBy.ToLower())
            {
                case "id":
                    query = query.OrderBy(m => m.Id);
                    break;
                case "placa":
                    query = query.OrderBy(m => m.Placa);
                    break;
                case "modelo":
                    query = query.OrderBy(m => m.Modelo);
                    break;
                case "ano":
                    query = query.OrderBy(m => m.Ano);
                    break;
                default:
                    query = query.OrderBy(m => m.Placa); // Default sort
                    break;
            }

            return await query.Skip(page * size).Take(size).ToListAsync();
        }
    }
}