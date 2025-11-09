using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Commands.Update;
public class UpdateLicencaCommandHandler : IRequestHandler<UpdateLicencaCommand, Unit>
{
    private readonly ILicencaRepository _licencaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLicencaCommandHandler(ILicencaRepository licencaRepository, IUnitOfWork unitOfWork)
    {
        _licencaRepository = licencaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateLicencaCommand request, CancellationToken cancellationToken)
    {
        var licenca = await _licencaRepository.GetByIdAsync(request.Id);
        if (licenca == null)
        {
            throw new InvalidOperationException($"Licença com Id {request.Id} não encontrada");
        }

        licenca.AtualizarDados(request.Tipo, request.DataExpiracao, request.MaximoInstalacoes, request.MaximoUsuarios);

        await _licencaRepository.UpdateAsync(licenca);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
