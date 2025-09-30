using System.ComponentModel.DataAnnotations;

namespace MottuLocation.DTOs
{
    public class SensorDTO : LinkedResourceBaseDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O código do sensor é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O código do sensor não pode exceder 255 caracteres.")]
        public string Codigo { get; set; } = string.Empty;

        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}