using MottuLocation.DTOs;

namespace MottuLocation.Services
{
    public interface IMovimentacaoService
    {
        Task<MovimentacaoDTO> RegistrarMovimentacaoAsync(string rfid, string sensorCodigo);
        Task<IEnumerable<MovimentacaoDTO>> ListarMovimentacoesPorMotoAsync(long motoId, int page, int size, string sortBy);
    }
}