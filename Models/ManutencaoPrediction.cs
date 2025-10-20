using Microsoft.ML.Data;

namespace MottuLocation.Models
{
    /// <summary>
    /// Classe que representa o resultado (a saída) da previsão do modelo.
    /// </summary>
    public class ManutencaoPrediction
    {
        /// <summary>
        /// O resultado da previsão (true ou false). O ML.NET preenche esta propriedade.
        /// O atributo [ColumnName("PredictedLabel")] é obrigatório para o ML.NET saber onde colocar o resultado.
        /// </summary>
        [ColumnName("PredictedLabel")]
        public bool PredictedLabel { get; set; }

        /// <summary>
        /// A pontuação ou probabilidade da previsão. Um valor mais próximo de 1 indica maior confiança na previsão "true".
        /// </summary>
        public float Score { get; set; }
    }
}
