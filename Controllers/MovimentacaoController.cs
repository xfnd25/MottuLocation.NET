using Microsoft.AspNetCore.Mvc;
using MottuLocation.DTOs;
using MottuLocation.Services;
using MottuLocation.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MottuLocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoService _movimentacaoService;

        public MovimentacaoController(IMovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        /// <summary>
        /// Registra uma nova movimentação de moto.
        /// </summary>
        /// <remarks>
        /// Este endpoint simula a leitura de uma tag RFID de uma moto por um sensor, 
        /// registrando a data e hora do evento.
        /// </remarks>
        /// <param name="request">Objeto contendo o RFID da moto e o código do sensor.</param>
        /// <returns>O registro da movimentação criada.</returns>
        /// <response code="201">Movimentação registrada com sucesso.</response>
        /// <response code="400">Se os dados da requisição forem inválidos.</response>
        /// <response code="404">Se a moto (via RFID) ou o sensor não forem encontrados.</response>
        [HttpPost(Name = "RegistrarMovimentacao")]
        [ProducesResponseType(typeof(MovimentacaoDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovimentacaoDTO>> RegistrarMovimentacao([FromBody] MovimentacaoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var movimentacaoDTO = await _movimentacaoService.RegistrarMovimentacaoAsync(request.Rfid, request.SensorCodigo);
                GenerateMovimentacaoLinks(movimentacaoDTO);
                return CreatedAtAction(nameof(ListarMovimentacoesPorMoto), new { motoId = movimentacaoDTO.MotoId }, movimentacaoDTO);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Lista as movimentações de uma moto específica.
        /// </summary>
        /// <param name="motoId">ID da moto para a qual se deseja obter o histórico.</param>
        /// <param name="page">Número da página (começando em 0).</param>
        /// <param name="size">Quantidade de itens por página.</param>
        /// <param name="sortBy">Campo para ordenação (padrão: "dataHora").</param>
        /// <returns>Uma lista paginada com o histórico de movimentações da moto.</returns>
        /// <response code="200">Retorna a lista de movimentações.</response>
        /// <response code="404">Se a moto com o ID informado não for encontrada.</response>
        [HttpGet("moto/{motoId}", Name = "ListarMovimentacoesPorMoto")]
        [ProducesResponseType(typeof(IEnumerable<MovimentacaoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> ListarMovimentacoesPorMoto(
            [FromRoute] long motoId,
            [FromQuery] int page = 0,
            [FromQuery] int size = 10,
            [FromQuery] string sortBy = "dataHora")
        {
            try
            {
                var movimentacoes = await _movimentacaoService.ListarMovimentacoesPorMotoAsync(motoId, page, size, sortBy);
                foreach (var movimentacao in movimentacoes)
                {
                    GenerateMovimentacaoLinks(movimentacao);
                }
                return Ok(movimentacoes);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        private void GenerateMovimentacaoLinks(MovimentacaoDTO movimentacao)
        {
            if (movimentacao == null) return;

            // Link para a moto relacionada
            movimentacao.Links.Add(new LinkDTO(Url.Link("GetMotoById", new { id = movimentacao.MotoId }), "related_moto", "GET"));

            // Link para o sensor relacionado
            movimentacao.Links.Add(new LinkDTO(Url.Link("GetSensorById", new { id = movimentacao.SensorId }), "related_sensor", "GET"));
        }
    }
}