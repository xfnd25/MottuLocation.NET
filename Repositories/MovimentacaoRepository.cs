using Microsoft.EntityFrameworkCore;
using MottuLocation.Data;
using MottuLocation.Entities;

namespace MottuLocation.Repositories
{
    public class MovimentacaoRepository : GenericRepository<Movimentacao>, IMovimentacaoRepository
    {
        private readonly new MottuLocationDbContext _context;

        public MovimentacaoRepository(MottuLocationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movimentacao>> GetByMotoIdAsync(long motoId, int page, int size, string sortBy)
        {
            var query = _context.Movimentacoes.Where(m => m.MotoId == motoId);

            switch (sortBy.ToLower())
            {
                case "id":
                    query = query.OrderBy(m => m.Id);
                    break;
                case "datahora":
                    query = query.OrderBy(m => m.DataHora);
                    break;
                default:
                    query = query.OrderByDescending(m => m.DataHora); // Default sort descending by date
                    break;
            }

            return await query.Skip(page * size).Take(size).ToListAsync();
        }
    }
}