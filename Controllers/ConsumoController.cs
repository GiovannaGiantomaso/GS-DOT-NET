using GsDotNet.Data;
using GsDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Consumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumoEnergia>>> GetConsumos()
        {
            return await _context.Consumos.ToListAsync();
        }

        // GET: api/Consumo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoEnergia>> GetConsumo(int id)
        {
            var consumo = await _context.Consumos.FindAsync(id);

            if (consumo == null)
            {
                return NotFound();
            }

            return consumo;
        }

        // POST: api/Consumo
        [HttpPost]
        public async Task<ActionResult<ConsumoEnergia>> CreateConsumo(ConsumoEnergia consumo)
        {
            consumo.DataRegistro = DateTime.Now;

            _context.Consumos.Add(consumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsumo), new { id = consumo.IdConsumo }, consumo);
        }


        // PUT: api/Consumo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumo(int id, ConsumoEnergia consumo)
        {
            if (id != consumo.IdConsumo)
            {
                return BadRequest();
            }

            _context.Entry(consumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Consumo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumo(int id)
        {
            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo == null)
            {
                return NotFound();
            }

            _context.Consumos.Remove(consumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsumoExists(int id)
        {
            return _context.Consumos.Any(e => e.IdConsumo == id);
        }
    }
}
