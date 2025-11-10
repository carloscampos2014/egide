using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Criar;

/// <summary>
/// Comando para criar um novo usuário.
/// </summary>
public record CriarUsuarioCommand(string Nome, string Email, string Senha) : IRequest<Guid>;