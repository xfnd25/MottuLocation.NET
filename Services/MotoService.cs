using AutoMapper;
using MottuLocation.DTOs;
using MottuLocation.Entities;
using MottuLocation.Repositories;
using MottuLocation.Exceptions;

namespace MottuLocation.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _motoRepository;
        private readonly IMapper _mapper;

        public MotoService(IMotoRepository motoRepository, IMapper mapper)
        {
            _motoRepository = motoRepository;
            _mapper = mapper;
        }

        public async Task<MotoDTO> CreateMotoAsync(MotoDTO motoDTO)
        {
            var moto = _mapper.Map<Moto>(motoDTO);
            moto.RfidTag = Guid.NewGuid().ToString(); // Gerando o RFID
            await _motoRepository.AddAsync(moto);
            return _mapper.Map<MotoDTO>(moto);
        }

        public async Task<MotoDTO> GetMotoByIdAsync(long id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
            {
                throw new ResourceNotFoundException($"Moto com ID {id} não encontrada.");
            }
            return _mapper.Map<MotoDTO>(moto);
        }

        public async Task<MotoDTO> UpdateMotoAsync(long id, MotoDTO motoDTO)
        {
            var existingMoto = await _motoRepository.GetByIdAsync(id);
            if (existingMoto == null)
            {
                throw new ResourceNotFoundException($"Moto com ID {id} não encontrada.");
            }

            // Atualize apenas as propriedades que podem ser modificadas
            existingMoto.Placa = motoDTO.Placa;
            existingMoto.Modelo = motoDTO.Modelo;
            existingMoto.Ano = motoDTO.Ano;
            existingMoto.Status = motoDTO.Status;
            existingMoto.Observacoes = motoDTO.Observacoes;

            await _motoRepository.UpdateAsync(existingMoto);
            return _mapper.Map<MotoDTO>(existingMoto);
        }

        public async Task DeleteMotoAsync(long id)
        {
            var motoToDelete = await _motoRepository.GetByIdAsync(id);
            if (motoToDelete == null)
            {
                throw new ResourceNotFoundException($"Moto com ID {id} não encontrada.");
            }
            await _motoRepository.DeleteAsync(motoToDelete);
        }

        public async Task<IEnumerable<MotoDTO>> ListMotosAsync(int page, int size, string sortBy, string? placaFiltro)
        {
            var motos = await _motoRepository.GetAllAsync(page, size, sortBy, placaFiltro);
            return _mapper.Map<IEnumerable<MotoDTO>>(motos);
        }
    }
}