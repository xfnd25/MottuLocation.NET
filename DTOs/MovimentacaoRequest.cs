using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class MovimentacaoRequest
    {
        [Required(ErrorMessage = "O RFID da moto é obrigatório.")]
        public string Rfid { get; set; } = string.Empty; // Inicialização para evitar o warning

        [Required(ErrorMessage = "O código do sensor é obrigatório.")]
        public string SensorCodigo { get; set; } = string.Empty; // Inicialização para evitar o warning
    }
}