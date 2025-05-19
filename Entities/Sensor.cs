using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MottuLocation.Entities
{
    [Index(nameof(Codigo), IsUnique = true)]
    public class Sensor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR2(255)")]
        public string Codigo { get; set; } = string.Empty;

        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public ICollection<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>();
    }
}