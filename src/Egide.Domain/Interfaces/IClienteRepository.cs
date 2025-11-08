using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces;

/// <summary>
/// Define o contrato para o repositório de Clientes,
/// abstraindo o acesso aos dados da camada de Domínio.
/// </summary>
public interface IClienteRepository
{
    /// <summary>
    /// Busca um cliente pelo seu Id.
    /// </summary>
    /// <param name="id">O Id único do cliente.</param>
    /// <returns>A entidade Cliente, se encontrada; caso contrário, null.</returns>
    Task<Cliente> GetByIdAsync(Guid id);

    /// <summary>
    /// Lista todos os clientes cadastrados.
    /// </summary>
    /// <returns>Uma coleção enumerável de todos os clientes.</returns>
    Task<IEnumerable<Cliente>> GetAllAsync();

    /// <summary>
    /// Adiciona um novo cliente ao repositório.
    /// </summary>
    /// <param name="cliente">A entidade cliente a ser adicionada.</param>
    Task AddAsync(Cliente cliente);

    /// <summary>
    /// Atualiza um cliente existente no repositório.
    /// </summary>
    /// <param name="cliente">A entidade cliente com os dados atualizados.</param>
    Task UpdateAsync(Cliente cliente);

    /// <summary>
    /// Remove um cliente do repositório pelo seu Id.
    /// </summary>
    /// <param name="id">O Id do cliente a ser removido.</param>
    Task DeleteAsync(Guid id);
}