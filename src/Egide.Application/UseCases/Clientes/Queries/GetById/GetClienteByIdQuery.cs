using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Queries.GetById;
public class GetClienteByIdQuery : IRequest<Cliente?>
{
    public GetClienteByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
