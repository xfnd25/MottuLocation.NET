using Microsoft.ML;
using MottuLocation.Models;
using System.Collections.Generic;

namespace MottuLocation.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly PredictionEngine<MotoData, ManutencaoPrediction> _predictionEngine;

        public PredictionService()
        {
            var mlContext = new MLContext();

            // Passo 1: Criar dados de treinamento fictícios.
            // Em um cenário real, estes dados viriam do seu banco de dados.
            var sampleData = new List<MotoData>
            {
                new MotoData { Ano = 2020, TotalMovimentacoes = 500, PrecisaManutencao = true },
                new MotoData { Ano = 2021, TotalMovimentacoes = 450, PrecisaManutencao = true },
                new MotoData { Ano = 2022, TotalMovimentacoes = 100, PrecisaManutencao = false },
                new MotoData { Ano = 2023, TotalMovimentacoes = 50, PrecisaManutencao = false },
                new MotoData { Ano = 2024, TotalMovimentacoes = 20, PrecisaManutencao = false },
                new MotoData { Ano = 2022, TotalMovimentacoes = 600, PrecisaManutencao = true },
                new MotoData { Ano = 2023, TotalMovimentacoes = 300, PrecisaManutencao = false }
            };

            var trainingData = mlContext.Data.LoadFromEnumerable(sampleData);

            // Passo 2: Definir o pipeline de treinamento.
            // Ele transforma os dados e escolhe um algoritmo de aprendizado.
            var pipeline = mlContext.Transforms.Concatenate("Features", nameof(MotoData.Ano), nameof(MotoData.TotalMovimentacoes))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(MotoData.PrecisaManutencao), featureColumnName: "Features"));

            // Passo 3: Treinar o modelo.
            var model = pipeline.Fit(trainingData);

            // Passo 4: Criar o "motor de previsão" para fazer previsões individuais.
            _predictionEngine = mlContext.Model.CreatePredictionEngine<MotoData, ManutencaoPrediction>(model);
        }

        /// <summary>
        /// Usa o motor de previsão treinado para prever a necessidade de manutenção.
        /// </summary>
        public ManutencaoPrediction Predict(MotoData motoData)
        {
            return _predictionEngine.Predict(motoData);
        }
    }
}
