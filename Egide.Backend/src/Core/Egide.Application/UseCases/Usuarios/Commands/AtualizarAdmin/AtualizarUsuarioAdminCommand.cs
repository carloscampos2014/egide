using Egide.Domain.Enums;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.AtualizarAdmin;

/// <summary>
/// Comando para um administrador atualizar os dados de um usuário.
/// </summary>
public record AtualizarUsuarioAdminCommand(Guid Id, string Nome, string Email, bool EstaAtivo, string Senha, AcaoTokenTenat Acao) : IRequest<Unit>;
