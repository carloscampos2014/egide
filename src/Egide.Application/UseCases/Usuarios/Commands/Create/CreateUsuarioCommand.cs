using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Create;
public class CreateUsuarioCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Senha { get; set; } = string.Empty;
}
