using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces;
public interface ISoftwareRepository
{
    Task<Software> GetByIdAsync(Guid id);
    Task<IEnumerable<Software>> GetAllAsync();
    Task AddAsync(Software software);
    Task UpdateAsync(Software software);
    Task DeleteAsync(Guid id);
}
