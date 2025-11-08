using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Queries.GetAll;
public class GetAllSoftwareQueryHandler : IRequestHandler<GetAllSoftwareQuery, IEnumerable<Software>>
{
    private readonly ISoftwareRepository _softwareRepository;

    public GetAllSoftwareQueryHandler(ISoftwareRepository softwareRepository)
    {
        _softwareRepository = softwareRepository;
    }

    public async Task<IEnumerable<Software>> Handle(GetAllSoftwareQuery request, CancellationToken cancellationToken)
    {
        return await _softwareRepository.GetAllAsync();
    }
}
