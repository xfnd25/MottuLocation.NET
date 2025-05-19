using MottuLocation.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuLocation.Repositories
{
    public interface IMovimentacaoRepository : IGenericRepository<Movimentacao>
    {
        Task<IEnumerable<Movimentacao>> GetByMotoIdAsync(long motoId, int page, int size, string sortBy);
    }
}   