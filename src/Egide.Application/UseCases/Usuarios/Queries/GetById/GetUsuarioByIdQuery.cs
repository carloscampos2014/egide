using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.GetById;
public class GetUsuarioByIdQuery : IRequest<Usuario?>
{
    public GetUsuarioByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
