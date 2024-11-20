using GsDotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GsDotNet.Repositories.Interfaces
{
    public interface IHistoricoConsumoRepository
    {
        Task<IEnumerable<HistoricoConsumo>> GetAllAsync();
        Task<HistoricoConsumo> GetByIdAsync(int id);
        Task AddAsync(HistoricoConsumo historico);
        Task UpdateAsync(HistoricoConsumo historico);
        Task DeleteAsync(int id);

        /// <summary>
        /// Calcula o total de consumo e a média mensal para o usuário e insere no histórico.
        /// </summary>
        /// <param name="idUsuario">ID do usuário.</param>
        Task AddHistoricoByUsuarioAsync(int idUsuario);
    }
}
