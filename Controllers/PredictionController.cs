using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuLocation.DTOs;
using MottuLocation.Models;
using MottuLocation.Services;

namespace MottuLocation.Controllers
{
    /// <summary>
    /// Controller para realizar previsões de manutenção de motos usando ML.NET.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;

        public PredictionController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        /// <summary>
        /// Prevê a necessidade de manutenção de uma moto.
        /// </summary>
        /// <remarks>
        /// Este endpoint utiliza um modelo de Machine Learning treinado para prever
        /// se uma moto precisa de manutenção com base em seu ano e histórico de movimentações.
        /// </remarks>
        /// <param name="request">Dados da moto para a previsão.</param>
        /// <returns>O resultado da previsão.</returns>
        /// <response code="200">Retorna a previsão de manutenção com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ManutencaoPrediction), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] PredictionRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapeia o DTO de requisição para o modelo de dados do ML.NET
            var motoData = new MotoData
            {
                Ano = request.Ano,
                TotalMovimentacoes = request.TotalMovimentacoes
            };

            var prediction = _predictionService.Predict(motoData);

            return Ok(prediction);
        }
    }
}
