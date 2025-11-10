using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces.Usuarios;

/// <summary>
/// Define o contrato para operações de leitura para a entidade Usuário.
/// </summary>
public interface IUsuarioReadRepository
{
    /// <summary>
    /// Obtém um usuário pelo seu ID.
    /// </summary>
    /// <param name="id">O ID do usuário.</param>
    /// <returns>O usuário, se encontrado; caso contrário, nulo.</returns>
    Task<Usuario?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém um usuário pelo seu endereço de e-mail.
    /// </summary>
    /// <param name="email">O e-mail do usuário.</param>
    /// <returns>O usuário, se encontrado; caso contrário, nulo.</returns>
    Task<Usuario?> ObterPorEmailAsync(string email);
}