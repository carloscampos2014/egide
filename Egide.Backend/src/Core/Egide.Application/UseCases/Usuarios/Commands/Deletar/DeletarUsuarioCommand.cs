using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Deletar;

/// <summary>
/// Comando para deletar um usuário.
/// </summary>
public record DeletarUsuarioCommand(Guid Id) : IRequest<Unit>;