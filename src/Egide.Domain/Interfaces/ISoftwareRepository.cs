using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces;

/// <summary>
/// Define o contrato para o repositório de Softwares,
/// abstraindo o acesso aos dados da camada de Domínio.
/// </summary>
public interface ISoftwareRepository
{
    /// <summary>
    /// Busca um software pelo seu Id.
    /// </summary>
    /// <param name="id">O Id único do software.</param>
    /// <returns>A entidade Software, se encontrada; caso contrário, null.</returns>
    Task<Software> GetByIdAsync(Guid id);

    /// <summary>
    /// Lista todos os softwares cadastrados.
    /// </summary>
    /// <returns>Uma coleção enumerável de todos os softwares.</returns>
    Task<IEnumerable<Software>> GetAllAsync();

    /// <summary>
    /// Adiciona um novo software ao repositório.
    /// </summary>
    /// <param name="software">A entidade software a ser adicionada.</param>
    Task AddAsync(Software software);

    /// <summary>
    /// Atualiza um software existente no repositório.
    /// </summary>
    /// <param name="software">A entidade software com os dados atualizados.</param>
    Task UpdateAsync(Software software);

    /// <summary>
    /// Remove um software do repositório pelo seu Id.
    /// </summary>
    /// <param name="id">O Id do software a ser removido.</param>
    Task DeleteAsync(Guid id);
}