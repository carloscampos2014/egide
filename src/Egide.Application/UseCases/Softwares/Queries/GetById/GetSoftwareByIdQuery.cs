using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Queries.GetById;
public class GetSoftwareByIdQuery : IRequest<Software?>
{
    public GetSoftwareByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
