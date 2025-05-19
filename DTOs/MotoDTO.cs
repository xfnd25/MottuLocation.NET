using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class MotoDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A placa não pode exceder 255 caracteres.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(2020, int.MaxValue, ErrorMessage = "O ano deve ser maior ou igual a 2020.")]
        public int Ano { get; set; }

        [MaxLength(255, ErrorMessage = "A tag RFID não pode exceder 255 caracteres.")]
        public string RfidTag { get; set; } = string.Empty; // Removido o [Required]

        public string Status { get; set; } = string.Empty;

        public string Observacoes { get; set; } = string.Empty;
    }
}