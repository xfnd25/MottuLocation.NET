using System;
using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class MovimentacaoDTO
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "O ID da Moto é obrigatório.")]
        public long MotoId { get; set; }

        [Required(ErrorMessage = "O ID do Sensor é obrigatório.")]
        public long SensorId { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now; // Define um valor padrão
    }
}