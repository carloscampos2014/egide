using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Commands.Create;
public class CreateLicencaCommandHandler: IRequestHandler<CreateLicencaCommand, Guid>
{
    private readonly ILicencaRepository _licencaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLicencaCommandHandler(ILicencaRepository licencaRepository, IClienteRepository clienteRepository, ISoftwareRepository softwareRepository, IUnitOfWork unitOfWork)
    {
        _licencaRepository = licencaRepository;
        _clienteRepository = clienteRepository;
        _softwareRepository = softwareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateLicencaCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
        if (cliente == null)
        {
            throw new InvalidOperationException($"Cliente com Id {request.ClienteId} não encontrado.");
        }

        var software = await _softwareRepository.GetByIdAsync(request.SoftwareId);
        if (software == null)
        {
            throw new InvalidOperationException($"Software com Id {request.SoftwareId} não encontrado.");
        }


        var licenca = new Licenca(
            clienteId: request.ClienteId,
            softwareId: request.SoftwareId,
            tipo: request.Tipo,
            dataExpiracao: request.DataExpiracao,
            maximoInstalacoes: request.MaximoInstalacoes,
            maximoUsuarios: request.MaximoUsuarios);
        await _licencaRepository.AddAsync(licenca);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return licenca.Id;
    }
}
