using AutoMapper;
using MottuLocation.DTOs;
using MottuLocation.Entities;
using MottuLocation.Repositories;
using MottuLocation.Exceptions;

namespace MottuLocation.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly IMotoRepository _motoRepository;
        private readonly ISensorRepository _sensorRepository;
        private readonly IMapper _mapper;

        public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository, IMotoRepository motoRepository, ISensorRepository sensorRepository, IMapper mapper)
        {
            _movimentacaoRepository = movimentacaoRepository;
            _motoRepository = motoRepository;
            _sensorRepository = sensorRepository;
            _mapper = mapper;
        }

        public async Task<MovimentacaoDTO> RegistrarMovimentacaoAsync(string rfid, string sensorCodigo)
        {
            var moto = await _motoRepository.GetByRfidTagAsync(rfid);
            if (moto == null)
            {
                throw new ResourceNotFoundException($"Moto com RFID {rfid} não encontrada.");
            }

            var sensor = await _sensorRepository.GetByCodigoAsync(sensorCodigo);
            if (sensor == null)
            {
                throw new ResourceNotFoundException($"Sensor com código {sensorCodigo} não encontrado.");
            }

            var movimentacao = new Movimentacao
            {
                MotoId = moto.Id,
                SensorId = sensor.Id,
                DataHora = DateTime.Now
            };

            await _movimentacaoRepository.AddAsync(movimentacao);
            return _mapper.Map<MovimentacaoDTO>(movimentacao);
        }

        public async Task<IEnumerable<MovimentacaoDTO>> ListarMovimentacoesPorMotoAsync(long motoId, int page, int size, string sortBy)
        {
            var moto = await _motoRepository.GetByIdAsync(motoId);
            if (moto == null)
            {
                throw new ResourceNotFoundException($"Moto com ID {motoId} não encontrada.");
            }
            var movimentacoes = await _movimentacaoRepository.GetByMotoIdAsync(motoId, page, size, sortBy);
            return _mapper.Map<IEnumerable<MovimentacaoDTO>>(movimentacoes);
        }
    }
}