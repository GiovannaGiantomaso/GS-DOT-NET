using GsDotNet.Data;
using GsDotNet.Models;
using GsDotNet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GsDotNet.Repositories.Implementations
{
    public class FeedbackConsumoRepository : IFeedbackConsumoRepository
    {
        private readonly AppDbContext _context;

        public FeedbackConsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedbackConsumo>> GetAllAsync()
        {
            return await _context.FeedbacksConsumo.ToListAsync();
        }

        public async Task<FeedbackConsumo> GetByIdAsync(int id)
        {
            return await _context.FeedbacksConsumo.FindAsync(id);
        }

        public async Task AddAsync(FeedbackConsumo feedback)
        {
            await _context.FeedbacksConsumo.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FeedbackConsumo feedback)
        {
            _context.FeedbacksConsumo.Update(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var feedback = await GetByIdAsync(id);
            _context.FeedbacksConsumo.Remove(feedback);
            await _context.SaveChangesAsync();
        }
    }
}
