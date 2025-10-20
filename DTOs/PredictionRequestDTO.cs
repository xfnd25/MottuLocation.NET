using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    /// <summary>
    /// Representa os dados de entrada para solicitar uma previsão de manutenção.
    /// </summary>
    public class PredictionRequestDTO
    {
        /// <summary>
        /// O ano de fabricação da moto.
        /// </summary>
        /// <example>2022</example>
        [Required]
        [Range(2000, 2100, ErrorMessage = "O ano deve ser válido.")]
        public int Ano { get; set; }

        /// <summary>
        /// O número total de movimentações que a moto já teve.
        /// </summary>
        /// <example>550</example>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O total de movimentações não pode ser negativo.")]
        public int TotalMovimentacoes { get; set; }
    }
}
