using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Atualizar;

/// <summary>
/// Comando para atualizar os dados de um usuário.
/// </summary>
public record AtualizarUsuarioCommand(Guid Id, string Nome, string SenhaAtual, string SenhaNova, string ConfirmarSenhaNova) : IRequest<Unit>;