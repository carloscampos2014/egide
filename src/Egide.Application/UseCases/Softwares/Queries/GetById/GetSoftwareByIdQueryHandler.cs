using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Queries.GetById;
public class GetSoftwareByIdQueryHandler : IRequestHandler<GetSoftwareByIdQuery, Software?>
{
    private readonly ISoftwareRepository _softwareRepository;

    public GetSoftwareByIdQueryHandler(ISoftwareRepository softwareRepository)
    {
        _softwareRepository = softwareRepository;
    }

    public async Task<Software?> Handle(GetSoftwareByIdQuery request, CancellationToken cancellationToken)
    {
        return await _softwareRepository.GetByIdAsync(request.Id);
    }
}
