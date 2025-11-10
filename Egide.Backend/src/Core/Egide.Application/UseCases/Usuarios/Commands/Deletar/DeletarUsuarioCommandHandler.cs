using Egide.Application.Abstractions;
using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Deletar;

public class DeletarUsuarioCommandHandler : IRequestHandler<DeletarUsuarioCommand, Unit>
{
    private readonly IUsuarioReadRepository _readRepository;
    private readonly IUsuarioWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarUsuarioCommandHandler(IUsuarioReadRepository readRepository, IUsuarioWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _readRepository.ObterPorIdAsync(request.Id);
        if (usuario is null)
        {
            throw new InvalidOperationException($"Usuário com Id {request.Id} não encontrado.");
        }

        await _writeRepository.RemoverAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}