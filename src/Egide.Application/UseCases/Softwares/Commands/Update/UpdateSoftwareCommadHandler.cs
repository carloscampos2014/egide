using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Commands.Update;
public class UpdateSoftwareCommadHandler :IRequestHandler<UpdateSoftwareCommad, Unit>
{
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSoftwareCommadHandler(ISoftwareRepository softwareRepository, IUnitOfWork unitOfWork)
    {
        _softwareRepository = softwareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateSoftwareCommad request, CancellationToken cancellationToken)
    {
        var software = await _softwareRepository.GetByIdAsync(request.Id);
        if (software == null)
        {
            throw new InvalidOperationException($"Software com Id {request.Id} não encontrado.");
        }

        software.AtualizarDados(
            titulo: request.Titulo,
            descricao: request.Descricao,
            versaoAtual: request.VersaoAtual);

        await _softwareRepository.UpdateAsync(software);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
