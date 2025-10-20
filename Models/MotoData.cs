using Microsoft.ML.Data;

namespace MottuLocation.Models
{
    /// <summary>
    /// Classe que representa os dados de entrada para o treinamento do modelo de ML.
    /// Cada propriedade é uma "coluna" ou "feature" que o modelo usará para aprender.
    /// </summary>
    public class MotoData
    {
        /// <summary>
        /// O ano de fabricação da moto.
        /// </summary>
        [LoadColumn(0)]
        public float Ano { get; set; }

        /// <summary>
        /// O número total de movimentações que a moto já teve.
        /// </summary>
        [LoadColumn(1)]
        public float TotalMovimentacoes { get; set; }

        /// <summary>
        /// A "resposta" que queremos prever. True se a moto precisou de manutenção.
        /// Este é o nosso "Label" (rótulo).
        /// </summary>
        [LoadColumn(2)]
        public bool PrecisaManutencao { get; set; }
    }
}
