using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Delete;
public class DeleteUsuarioCommand : IRequest<Unit>
{
    public DeleteUsuarioCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
