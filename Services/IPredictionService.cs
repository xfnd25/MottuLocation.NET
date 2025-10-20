using MottuLocation.Models;

namespace MottuLocation.Services
{
    /// <summary>
    /// Contrato para o serviço de previsão de manutenção.
    /// </summary>
    public interface IPredictionService
    {
        /// <summary>
        /// Realiza uma previsão de necessidade de manutenção com base nos dados de uma moto.
        /// </summary>
        /// <param name="motoData">Os dados da moto para a previsão.</param>
        /// <returns>O resultado da previsão.</returns>
        ManutencaoPrediction Predict(MotoData motoData);
    }
}
