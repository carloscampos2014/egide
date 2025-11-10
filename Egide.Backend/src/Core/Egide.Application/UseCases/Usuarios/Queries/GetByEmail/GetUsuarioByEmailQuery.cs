using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.GetByEmail;
public record GetUsuarioByEmailQuery(string Email) : IRequest<Domain.Entities.Usuario?>;
