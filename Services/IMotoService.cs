using MottuLocation.DTOs;

namespace MottuLocation.Services
{
    public interface IMotoService
    {
        Task<MotoDTO> CreateMotoAsync(MotoDTO motoDTO);
        Task<MotoDTO> GetMotoByIdAsync(long id);
        Task<MotoDTO> UpdateMotoAsync(long id, MotoDTO motoDTO);
        Task DeleteMotoAsync(long id);
        Task<IEnumerable<MotoDTO>> ListMotosAsync(int page, int size, string sortBy, string? placaFiltro);
    }
}