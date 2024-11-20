using GsDotNet.Data;
using GsDotNet.Models;
using GsDotNet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GsDotNet.Repositories.Implementations
{
    public class HistoricoConsumoRepository : IHistoricoConsumoRepository
    {
        private readonly AppDbContext _context;

        public HistoricoConsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricoConsumo>> GetAllAsync()
        {
            return await _context.HistoricosConsumo.ToListAsync();
        }

        public async Task<HistoricoConsumo> GetByIdAsync(int id)
        {
            return await _context.HistoricosConsumo.FindAsync(id);
        }

        public async Task AddAsync(HistoricoConsumo historico)
        {
            await _context.HistoricosConsumo.AddAsync(historico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HistoricoConsumo historico)
        {
            _context.HistoricosConsumo.Update(historico);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var historico = await GetByIdAsync(id);
            if (historico != null)
            {
                _context.HistoricosConsumo.Remove(historico);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddHistoricoByUsuarioAsync(int idUsuario)
        {
            // Busca todos os registros de consumo do usuário
            var consumos = await _context.Consumos
                .Where(c => c.IdUsuario == idUsuario)
                .ToListAsync();

            if (consumos == null || !consumos.Any())
                throw new InvalidOperationException("Nenhum registro de consumo encontrado para o usuário.");

            // Calcula o total de consumo e a média mensal
            var totalConsumo = consumos.Sum(c => c.ConsumoKwh);
            var mediaMensal = consumos.Count > 0 ? totalConsumo / consumos.Count : 0;

            // Cria o registro no histórico
            var historico = new HistoricoConsumo
            {
                IdUsuario = idUsuario,
                TotalConsumo = totalConsumo,
                MediaMensal = mediaMensal
            };

            await AddAsync(historico);
        }
    }
}
