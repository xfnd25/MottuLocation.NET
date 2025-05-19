using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; // Adicione esta linha se ainda não estiver lá

namespace MottuLocation.Entities
{
    [Index(nameof(Placa), IsUnique = true)]
    [Index(nameof(RfidTag), IsUnique = true)]
    public class Moto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR2(255)")]
        public string Placa { get; set; } = string.Empty; // Inicialização

        [Required]
        public string Modelo { get; set; } = string.Empty; // Inicialização

        [Range(2020, int.MaxValue)]
        public int Ano { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR2(255)")]
        public string RfidTag { get; set; } = string.Empty; // Inicialização

        public string Status { get; set; } = string.Empty; // Inicialização

        public string Observacoes { get; set; } = string.Empty; // Inicialização

        public ICollection<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>(); // Inicialização
    }
}