using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Commands.Delete;
public class DeleteLicencaCommandHandler : IRequestHandler<DeleteLicencaCommand, Unit>
{
    private readonly ILicencaRepository _licencaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLicencaCommandHandler(ILicencaRepository licencaRepository, IUnitOfWork unitOfWork)
    {
        _licencaRepository = licencaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteLicencaCommand request, CancellationToken cancellationToken)
    {
        var licenca = await _licencaRepository.GetByIdAsync(request.Id);
        if (licenca == null)
        {
            throw new InvalidOperationException($"Licença com Id {request.Id} não encontrada");
        }

        await _licencaRepository.DeleteAsync(request.Id);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
