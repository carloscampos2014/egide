using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.GetById;
public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, Usuario?>
{
    private IUsuarioRepository _usuarioRepository;

    public GetUsuarioByIdQueryHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        return await _usuarioRepository.GetByIdAsync(request.Id);
    }
}
