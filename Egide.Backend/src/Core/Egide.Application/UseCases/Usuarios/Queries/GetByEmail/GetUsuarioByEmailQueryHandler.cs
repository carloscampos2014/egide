using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.GetByEmail;
public class GetUsuarioByEmailQueryHandler : IRequestHandler<GetUsuarioByEmailQuery, Domain.Entities.Usuario?>
{
    private readonly IUsuarioReadRepository _readRepository;

    public GetUsuarioByEmailQueryHandler(IUsuarioReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Domain.Entities.Usuario?> Handle(GetUsuarioByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _readRepository.ObterPorEmailAsync(request.Email);
    }
}
