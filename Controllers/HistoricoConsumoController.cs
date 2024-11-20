using GsDotNet.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GsDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoConsumoController : ControllerBase
    {
        private readonly IHistoricoConsumoRepository _repository;

        public HistoricoConsumoController(IHistoricoConsumoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoConsumo>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoConsumo>> GetById(int id)
        {
            var historico = await _repository.GetByIdAsync(id);
            if (historico == null)
                return NotFound();
            return Ok(historico);
        }


        [HttpPost("usuario/{idUsuario}")]
        public async Task<ActionResult> AddHistoricoByUsuario(int idUsuario)
        {
            try
            {
                await _repository.AddHistoricoByUsuarioAsync(idUsuario);
                return Ok("Histórico de consumo calculado e adicionado com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HistoricoConsumo historico)
        {
            if (id != historico.IdHistorico)
                return BadRequest();
            await _repository.UpdateAsync(historico);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
