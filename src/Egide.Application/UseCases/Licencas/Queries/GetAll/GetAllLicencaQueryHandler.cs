using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Queries.GetAll;
public class GetAllLicencaQueryHandler : IRequestHandler<GetAllLicencaQuery, IEnumerable<Licenca>>
{
    private readonly ILicencaRepository _licencaRepository;

    public GetAllLicencaQueryHandler(ILicencaRepository licencaRepository)
    {
        _licencaRepository = licencaRepository;
    }

    public async Task<IEnumerable<Licenca>> Handle(GetAllLicencaQuery request, CancellationToken cancellationToken)
    {
        return await _licencaRepository.GetAllAsync(request.Filtro);
    }
}
