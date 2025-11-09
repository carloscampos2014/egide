using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Update;
public class UpdateUsuarioCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string SenhaAtual { get; set; } = string.Empty;

    public string NovaSenha { get; set; } = string.Empty;

    public string ConfirmarSenha { get; set; } = string.Empty;
}
