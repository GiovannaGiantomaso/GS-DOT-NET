using System.Collections.Generic;
using System.Threading.Tasks;
using GsDotNet.Models;

namespace GsDotNet.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioEnergia>> GetAllUsuariosAsync();
        Task<UsuarioEnergia> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(UsuarioEnergia usuario);
        Task UpdateUsuarioAsync(UsuarioEnergia usuario);
        Task DeleteUsuarioAsync(int id);
    }
}
