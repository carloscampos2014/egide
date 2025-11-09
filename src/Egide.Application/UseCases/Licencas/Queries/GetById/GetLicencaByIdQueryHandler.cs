using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Queries.GetById;
public class GetLicencaByIdQueryHandler : IRequestHandler<GetLicencaByIdQuery, Licenca?>
{
    private readonly ILicencaRepository _licencaRepository;

    public GetLicencaByIdQueryHandler(ILicencaRepository licencaRepository)
    {
        _licencaRepository = licencaRepository;
    }

    public async Task<Licenca?> Handle(GetLicencaByIdQuery request, CancellationToken cancellationToken)
    {
        return await _licencaRepository.GetByIdAsync(request.Id);
    }
}
