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
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        /// <summary>
        /// Cria uma nova moto no sistema.
        /// </summary>
        [HttpPost(Name = "CreateMoto")]
        [ProducesResponseType(typeof(MotoDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotoDTO>> CreateMoto([FromBody] MotoDTO motoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdMoto = await _motoService.CreateMotoAsync(motoDTO);
            
            GenerateMotoLinks(createdMoto);

            return CreatedAtAction(nameof(GetMotoById), new { id = createdMoto.Id }, createdMoto);
        }

        /// <summary>
        /// Busca uma moto específica pelo seu ID.
        /// </summary>
        [HttpGet("{id}", Name = "GetMotoById")]
        [ProducesResponseType(typeof(MotoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoDTO>> GetMotoById(long id)
        {
            var moto = await _motoService.GetMotoByIdAsync(id);
            if (moto == null)
            {
                return NotFound();
            }

            GenerateMotoLinks(moto);
            
            return Ok(moto);
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        [HttpPut("{id}", Name = "UpdateMoto")]
        [ProducesResponseType(typeof(MotoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoDTO>> UpdateMoto(long id, [FromBody] MotoDTO motoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedMoto = await _motoService.UpdateMotoAsync(id, motoDTO);
                GenerateMotoLinks(updatedMoto);
                return Ok(updatedMoto);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Remove uma moto do sistema.
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteMoto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMoto(long id)
        {
            try
            {
                await _motoService.DeleteMotoAsync(id);
                return NoContent();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Lista as motos com suporte a paginação, ordenação e filtro.
        /// </summary>
        [HttpGet(Name = "ListMotos")]
        [ProducesResponseType(typeof(IEnumerable<MotoDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MotoDTO>>> ListMotos(
            [FromQuery] int page = 0,
            [FromQuery] int size = 10,
            [FromQuery] string sortBy = "placa",
            [FromQuery] string? placaFiltro = null)
        {
            var motos = await _motoService.ListMotosAsync(page, size, sortBy, placaFiltro);
            
            foreach (var moto in motos)
            {
                GenerateMotoLinks(moto);
            }

            return Ok(motos);
        }

        private void GenerateMotoLinks(MotoDTO moto)
        {
            if (moto == null) return;

            moto.Links.Add(new LinkDTO(Url.Link("GetMotoById", new { id = moto.Id }), "self", "GET"));
            moto.Links.Add(new LinkDTO(Url.Link("UpdateMoto", new { id = moto.Id }), "update_moto", "PUT"));
            moto.Links.Add(new LinkDTO(Url.Link("DeleteMoto", new { id = moto.Id }), "delete_moto", "DELETE"));
        }
    }
}