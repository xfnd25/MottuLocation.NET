using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MottuLocation.Entities
{
    public class Movimentacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Moto")]
        public long MotoId { get; set; }
        public Moto Moto { get; set; } = null!; // Operador null-forgiving

        [Required]
        [ForeignKey("Sensor")]
        public long SensorId { get; set; }
        public Sensor Sensor { get; set; } = null!; // Operador null-forgiving

        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}