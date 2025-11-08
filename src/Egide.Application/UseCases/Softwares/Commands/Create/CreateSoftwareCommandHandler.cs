using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Commands.Create;
public class CreateSoftwareCommandHandler : IRequestHandler<CreateSoftwareCommand, Guid>
{
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSoftwareCommandHandler(ISoftwareRepository softwareRepository, IUnitOfWork unitOfWork)
    {
        _softwareRepository = softwareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateSoftwareCommand request, CancellationToken cancellationToken)
    {
        var software = new Software(
            titulo: request.Titulo,
            descricao: request.Descricao,
            versaoAtual: request.VersaoAtual
         );

        await _softwareRepository.AddAsync(software);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return software.Id;
    }
}
