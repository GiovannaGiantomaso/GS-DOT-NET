using System.Collections.Generic;
using System.Threading.Tasks;
using GsDotNet.Data;
using GsDotNet.Models;
using GsDotNet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GsDotNet.Repositories.Implementations
{
    public class ConsumoRepository : IConsumoRepository
    {
        private readonly AppDbContext _context;

        public ConsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetAllConsumosAsync()
        {
            return await _context.Consumos.ToListAsync();
        }

        public async Task<ConsumoEnergia> GetConsumoByIdAsync(int id)
        {
            return await _context.Consumos.FindAsync(id);
        }

        public async Task AddConsumoAsync(ConsumoEnergia consumo)
        {
            await _context.Consumos.AddAsync(consumo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateConsumoAsync(ConsumoEnergia consumo)
        {
            _context.Consumos.Update(consumo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConsumoAsync(int id)
        {
            var consumo = await GetConsumoByIdAsync(id);
            _context.Consumos.Remove(consumo);
            await _context.SaveChangesAsync();
        }
    }
}
