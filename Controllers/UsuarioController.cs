using Microsoft.AspNetCore.Mvc;
using GsDotNet.Data;
using GsDotNet.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
                                      .Include(u => u.Consumos) // Inclui os consumos
                                      .ToListAsync();

        var resultado = usuarios.Select(usuario => new
        {
            Usuario = new
            {
                usuario.IdUsuario,
                usuario.Nome,
                usuario.Email
            },
            Consumos = usuario.Consumos,
            HistoricosConsumo = _context.HistoricosConsumo.Where(h => h.IdUsuario == usuario.IdUsuario).ToList(),
            FeedbacksConsumo = _context.FeedbacksConsumo.Where(f => f.IdUsuario == usuario.IdUsuario).ToList()
        });

        return Ok(resultado);
    }


    // GET: api/Usuario/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios
                                    .Include(u => u.Consumos) // Inclui os consumos
                                    .FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (usuario == null)
        {
            return NotFound();
        }

        var resultado = new
        {
            Usuario = new
            {
                usuario.IdUsuario,
                usuario.Nome,
                usuario.Email
            },
            Consumos = usuario.Consumos,
            HistoricosConsumo = await _context.HistoricosConsumo.Where(h => h.IdUsuario == id).ToListAsync(),
            FeedbacksConsumo = await _context.FeedbacksConsumo.Where(f => f.IdUsuario == id).ToListAsync()
        };

        return Ok(resultado);
    }


    // POST: api/Usuario
    [HttpPost]
    public async Task<ActionResult<UsuarioEnergia>> CreateUsuario(UsuarioEnergia usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
    }

    // PUT: api/Usuario/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, UsuarioEnergia usuario)
    {
        if (id != usuario.IdUsuario)
        {
            return BadRequest();
        }

        _context.Entry(usuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(id))
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

    // DELETE: api/Usuario/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuarios.Any(e => e.IdUsuario == id);
    }
}
