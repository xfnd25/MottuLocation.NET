
using MottuLocation.DTOs;

namespace MottuLocation.Services
{
    public interface ISensorService
    {
        Task<SensorDTO> CreateSensorAsync(SensorDTO sensorDTO);
        Task<SensorDTO> GetSensorByIdAsync(long id);
        Task<SensorDTO> UpdateSensorAsync(long id, SensorDTO sensorDTO);
        Task DeleteSensorAsync(long id);
        Task<IEnumerable<SensorDTO>> ListSensorsAsync(int page, int size, string sortBy, string? codigoFiltro);
    }
}