using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.GetById;
public record GetUsuarioByIdQuery(Guid Id) : IRequest<Domain.Entities.Usuario?>;
