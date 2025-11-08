using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Delete;
public class DeleteClienteCommand : IRequest<Unit>
{
    public DeleteClienteCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
