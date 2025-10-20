using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class SensorDTO : LinkedResourceBaseDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// Código único de identificação do sensor.
        /// </summary>
        /// <example>PATIO_B05</example>
        [Required(ErrorMessage = "O código do sensor é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O código do sensor não pode exceder 255 caracteres.")]
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Coordenada X da posição do sensor no pátio.
        /// </summary>
        /// <example>150</example>
        public int PosicaoX { get; set; }
        
        /// <summary>
        /// Coordenada Y da posição do sensor no pátio.
        /// </summary>
        /// <example>75</example>
        public int PosicaoY { get; set; }

        /// <summary>
        /// Descrição da localização ou função do sensor.
        /// </summary>
        /// <example>Sensor localizado na entrada do pátio B</example>
        public string Descricao { get; set; } = string.Empty;
    }
}
