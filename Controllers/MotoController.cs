using Microsoft.AspNetCore.Mvc;
using MottuLocation.DTOs;
using MottuLocation.Services;
using System.ComponentModel.DataAnnotations;
using MottuLocation.Exceptions; // Certifique-se desta linha
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MottuLocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotoDTO>> CreateMoto([FromBody] MotoDTO motoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdMoto = await _motoService.CreateMotoAsync(motoDTO);
            return CreatedAtAction(nameof(GetMotoById), new { id = createdMoto.Id }, createdMoto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoDTO>> GetMotoById(long id)
        {
            var moto = await _motoService.GetMotoByIdAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            return Ok(moto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return Ok(updatedMoto);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MotoDTO>>> ListMotos(
            [FromQuery] int page = 0,
            [FromQuery] int size = 10,
            [FromQuery] string sortBy = "placa",
            [FromQuery] string? placaFiltro = null)
        {
            var motos = await _motoService.ListMotosAsync(page, size, sortBy, placaFiltro);
            return Ok(motos);
        }
    }
}