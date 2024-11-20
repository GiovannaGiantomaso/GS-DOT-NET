using GsDotNet.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GsDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackConsumoController : ControllerBase
    {
        private readonly IFeedbackConsumoRepository _repository;

        public FeedbackConsumoController(IFeedbackConsumoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackConsumo>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackConsumo>> GetById(int id)
        {
            var feedback = await _repository.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();
            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult> Add(FeedbackConsumo feedback)
        {
            await _repository.AddAsync(feedback);
            return CreatedAtAction(nameof(GetById), new { id = feedback.IdFeedback }, feedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FeedbackConsumo feedback)
        {
            if (id != feedback.IdFeedback)
                return BadRequest();
            await _repository.UpdateAsync(feedback);
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
