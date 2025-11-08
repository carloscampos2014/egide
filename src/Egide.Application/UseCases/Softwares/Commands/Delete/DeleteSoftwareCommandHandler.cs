using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Commands.Delete;
public class DeleteSoftwareCommandHandler : IRequestHandler<DeleteSoftwareCommand, Unit>
{
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSoftwareCommandHandler(ISoftwareRepository softwareRepository, IUnitOfWork unitOfWork)
    {
        _softwareRepository = softwareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteSoftwareCommand request, CancellationToken cancellationToken)
    {
        var software = await _softwareRepository.GetByIdAsync(request.Id);
        if (software == null)
        {
            throw new InvalidOperationException($"Software com Id {request.Id} não encontrado.");
        }

        await _softwareRepository.DeleteAsync(request.Id);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
