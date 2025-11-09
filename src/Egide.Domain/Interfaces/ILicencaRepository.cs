using Egide.Domain.Entities;
using Egide.Domain.Enums;

namespace Egide.Domain.Interfaces;
public interface ILicencaRepository
{
    Task<Licenca> GetByIdAsync(Guid id);

    Task<IEnumerable<Licenca>> GetAllAsync(FiltroLicenca filtro);

    Task AddAsync(Licenca licenca);

    Task UpdateAsync(Licenca licenca);

    Task DeleteAsync(Guid id);
}
