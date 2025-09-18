using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class MotoDTO : LinkedResourceBaseDTO
    {
        /// <summary>
        /// ID único da moto.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Placa de identificação da moto (deve ser única).
        /// </summary>
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A placa não pode exceder 255 caracteres.")]
        public string Placa { get; set; } = string.Empty;

        /// <summary>
        /// Modelo da moto.
        /// </summary>
        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        /// <summary>
        /// Ano de fabricação da moto.
        /// </summary>
        [Range(2020, int.MaxValue, ErrorMessage = "O ano deve ser maior ou igual a 2020.")]
        public int Ano { get; set; }

        /// <summary>
        /// Tag RFID associada à moto (gerado automaticamente).
        /// </summary>
        [MaxLength(255, ErrorMessage = "A tag RFID não pode exceder 255 caracteres.")]
        public string RfidTag { get; set; } = string.Empty;

        /// <summary>
        /// Status atual da moto (ex: DISPONIVEL, EM_MANUTENCAO).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Observações gerais sobre a moto.
        /// </summary>
        public string Observacoes { get; set; } = string.Empty;
    }
}