using Microsoft.AspNetCore.Mvc;
using GsDotNet.Data;
using GsDotNet.Models;
using System.Collections.Generic;
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
    public async Task<ActionResult<IEnumerable<UsuarioEnergia>>> GetUsuarios()
    {
        return await _context.Usuarios
                             .Include(u => u.Consumos)
                             .ToListAsync();
    }


    // GET: api/Usuario/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioEnergia>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios
                                    .Include(u => u.Consumos)
                                    .FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
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
