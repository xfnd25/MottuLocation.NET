using Microsoft.AspNetCore.Mvc;
using MottuLocation.DTOs;
using MottuLocation.Services;
using System.ComponentModel.DataAnnotations; // Você pode remover esta linha, pois o [Required] está no DTO
using MottuLocation.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MottuLocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoService _movimentacaoService;

        public MovimentacaoController(IMovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovimentacaoDTO>> RegistrarMovimentacao([FromBody] MovimentacaoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var movimentacaoDTO = await _movimentacaoService.RegistrarMovimentacaoAsync(request.Rfid, request.SensorCodigo);
                return CreatedAtAction(nameof(ListarMovimentacoesPorMoto), new { motoId = movimentacaoDTO.MotoId }, movimentacaoDTO);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("moto/{motoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> ListarMovimentacoesPorMoto(
            [FromRoute] long motoId,
            [FromQuery] int page = 0,
            [FromQuery] int size = 10,
            [FromQuery] string sortBy = "dataHora")
        {
            try
            {
                var movimentacoes = await _movimentacaoService.ListarMovimentacoesPorMotoAsync(motoId, page, size, sortBy);
                return Ok(movimentacoes);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}