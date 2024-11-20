using GsDotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GsDotNet.Repositories.Interfaces
{
    public interface IFeedbackConsumoRepository
    {
        Task<IEnumerable<FeedbackConsumo>> GetAllAsync();
        Task<FeedbackConsumo> GetByIdAsync(int id);
        Task AddAsync(FeedbackConsumo feedback);
        Task UpdateAsync(FeedbackConsumo feedback);
        Task DeleteAsync(int id);
    }
}
