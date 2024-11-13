using System.Collections.Generic;
using System.Threading.Tasks;
using GsDotNet.Models;

namespace GsDotNet.Repositories.Interfaces
{
    public interface IConsumoRepository
    {
        Task<IEnumerable<ConsumoEnergia>> GetAllConsumosAsync();
        Task<ConsumoEnergia> GetConsumoByIdAsync(int id);
        Task AddConsumoAsync(ConsumoEnergia consumo);
        Task UpdateConsumoAsync(ConsumoEnergia consumo);
        Task DeleteConsumoAsync(int id);
    }
}
