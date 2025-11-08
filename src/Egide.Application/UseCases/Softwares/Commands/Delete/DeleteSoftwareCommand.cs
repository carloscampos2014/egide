using MediatR;

namespace Egide.Application.UseCases.Softwares.Commands.Delete;
public class DeleteSoftwareCommand : IRequest<Unit>
{
    public DeleteSoftwareCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
