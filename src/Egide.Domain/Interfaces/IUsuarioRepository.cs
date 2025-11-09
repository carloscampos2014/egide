using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces;
public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);

    Task DeleteAsync(Guid id);

    Task<Usuario> GetByIdAsync(Guid id);

    Task UpdateAsync(Usuario usuario);
}
