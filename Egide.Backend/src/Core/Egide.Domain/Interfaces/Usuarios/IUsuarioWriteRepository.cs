using Egide.Domain.Entities;

namespace Egide.Domain.Interfaces.Usuarios;

/// <summary>
/// Define o contrato para operações de escrita para a entidade Usuário.
/// </summary>
public interface IUsuarioWriteRepository
{
    /// <summary>
    /// Adiciona um novo usuário.
    /// </summary>
    /// <param name="usuario">O usuário a ser adicionado.</param>
    Task AdicionarAsync(Usuario usuario);

    /// <summary>
    /// Atualiza um usuário existente.
    /// </summary>
    /// <param name="usuario">O usuário a ser atualizado.</param>
    Task AtualizarAsync(Usuario usuario);

    /// <summary>
    /// Remove um usuário.
    /// </summary>
    /// <param name="id">O ID do usuário a ser removido.</param>
    Task RemoverAsync(Guid id);
}