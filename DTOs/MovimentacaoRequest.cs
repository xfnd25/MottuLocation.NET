using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class MovimentacaoRequest
    {
        /// <summary>
        /// A tag RFID única da moto que está se movimentando.
        /// </summary>
        /// <example>f47ac10b-58cc-4372-a567-0e02b2c3d479</example>
        [Required(ErrorMessage = "O RFID da moto é obrigatório.")]
        public string Rfid { get; set; } = string.Empty;

        /// <summary>
        /// O código único do sensor que detectou a movimentação.
        /// </summary>
        /// <example>PATIO_A01</example>
        [Required(ErrorMessage = "O código do sensor é obrigatório.")]
        public string SensorCodigo { get; set; } = string.Empty;
    }
}
