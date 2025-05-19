using AutoMapper;
using MottuLocation.DTOs;
using MottuLocation.Entities;
using MottuLocation.Repositories;
using MottuLocation.Exceptions;

namespace MottuLocation.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly IMapper _mapper;

        public SensorService(ISensorRepository sensorRepository, IMapper mapper)
        {
            _sensorRepository = sensorRepository;
            _mapper = mapper;
        }

        public async Task<SensorDTO> CreateSensorAsync(SensorDTO sensorDTO)
        {
            var sensor = _mapper.Map<Sensor>(sensorDTO);
            await _sensorRepository.AddAsync(sensor);
            return _mapper.Map<SensorDTO>(sensor);
        }

        public async Task<SensorDTO> GetSensorByIdAsync(long id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null)
            {
                throw new ResourceNotFoundException($"Sensor com ID {id} não encontrado.");
            }
            return _mapper.Map<SensorDTO>(sensor);
        }

        public async Task<SensorDTO> UpdateSensorAsync(long id, SensorDTO sensorDTO)
        {
            var existingSensor = await _sensorRepository.GetByIdAsync(id);
            if (existingSensor == null)
            {
                throw new ResourceNotFoundException($"Sensor com ID {id} não encontrado.");
            }
            _mapper.Map(sensorDTO, existingSensor);
            await _sensorRepository.UpdateAsync(existingSensor);
            return _mapper.Map<SensorDTO>(existingSensor);
        }

        public async Task DeleteSensorAsync(long id)
        {
            var sensorToDelete = await _sensorRepository.GetByIdAsync(id);
            if (sensorToDelete == null)
            {
                throw new ResourceNotFoundException($"Sensor com ID {id} não encontrado.");
            }
            await _sensorRepository.DeleteAsync(sensorToDelete);
        }

        public async Task<IEnumerable<SensorDTO>> ListSensorsAsync(int page, int size, string sortBy, string? codigoFiltro)
        {
            var sensors = await _sensorRepository.GetAllAsync(page, size, sortBy, codigoFiltro);
            return _mapper.Map<IEnumerable<SensorDTO>>(sensors);
        }
    }
}