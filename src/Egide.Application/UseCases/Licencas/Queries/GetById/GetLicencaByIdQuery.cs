using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Queries.GetById;
public class GetLicencaByIdQuery : IRequest<Licenca?>
{
    public GetLicencaByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
