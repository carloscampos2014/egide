using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.GetById;
public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, Domain.Entities.Usuario?>
{
    private readonly IUsuarioReadRepository _readRepository;

    public GetUsuarioByIdQueryHandler(IUsuarioReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Domain.Entities.Usuario?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        return await _readRepository.ObterPorIdAsync(request.Id);
    }
}
