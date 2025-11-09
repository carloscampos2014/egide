using MediatR;

namespace Egide.Application.UseCases.Licencas.Commands.Delete;
public class DeleteLicencaCommand : IRequest<Unit>
{
    public DeleteLicencaCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
