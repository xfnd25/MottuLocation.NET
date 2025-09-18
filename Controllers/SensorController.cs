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
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Cria um novo sensor no sistema.
        /// </summary>
        /// <param name="sensorDTO">Objeto com os dados para a criação do novo sensor.</param>
        /// <returns>O sensor recém-criado.</returns>
        /// <response code="201">Retorna o sensor recém-criado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(SensorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SensorDTO>> CreateSensor([FromBody] SensorDTO sensorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdSensor = await _sensorService.CreateSensorAsync(sensorDTO);
            return CreatedAtAction(nameof(GetSensorById), new { id = createdSensor.Id }, createdSensor);
        }

        /// <summary>
        /// Busca um sensor específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do sensor a ser buscado.</param>
        /// <returns>Os dados do sensor encontrado.</returns>
        /// <response code="200">Sensor encontrado com sucesso.</response>
        /// <response code="404">Nenhum sensor encontrado com o ID informado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SensorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorDTO>> GetSensorById(long id)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }

        /// <summary>
        /// Atualiza os dados de um sensor existente.
        /// </summary>
        /// <param name="id">ID do sensor a ser atualizado.</param>
        /// <param name="sensorDTO">Objeto com os novos dados do sensor.</param>
        /// <returns>O sensor com os dados atualizados.</returns>
        /// <response code="200">Dados do sensor atualizados com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="404">Nenhum sensor encontrado com o ID informado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SensorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorDTO>> UpdateSensor(long id, [FromBody] SensorDTO sensorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedSensor = await _sensorService.UpdateSensorAsync(id, sensorDTO);
                return Ok(updatedSensor);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Remove um sensor do sistema.
        /// </summary>
        /// <param name="id">ID do sensor a ser removido.</param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">Sensor removido com sucesso.</response>
        /// <response code="404">Nenhum sensor encontrado com o ID informado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSensor(long id)
        {
            try
            {
                await _sensorService.DeleteSensorAsync(id);
                return NoContent();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Lista os sensores com suporte a paginação, ordenação e filtro.
        /// </summary>
        /// <param name="page">Número da página (começando em 0).</param>
        /// <param name="size">Quantidade de itens por página.</param>
        /// <param name="sortBy">Campo para ordenação (padrão: "codigo").</param>
        /// <param name="codigoFiltro">Filtra os sensores pelo código informado.</param>
        /// <returns>Uma lista paginada de sensores.</returns>
        /// <response code="200">Retorna a lista de sensores.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SensorDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SensorDTO>>> ListSensors(
            [FromQuery] int page = 0,
            [FromQuery] int size = 10,
            [FromQuery] string sortBy = "codigo",
            [FromQuery] string? codigoFiltro = null)
        {
            var sensors = await _sensorService.ListSensorsAsync(page, size, sortBy, codigoFiltro);
            return Ok(sensors);
        }
    }
}