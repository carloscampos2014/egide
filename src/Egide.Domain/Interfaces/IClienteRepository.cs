using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces;
public interface IClienteRepository
{
    Task<Cliente> GetByIdAsync(Guid id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task AddAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(Guid id);
}
