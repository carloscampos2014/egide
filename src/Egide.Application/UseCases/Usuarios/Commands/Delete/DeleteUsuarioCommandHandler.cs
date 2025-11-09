using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Delete;
public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Unit>
{
    private IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
        if (usuario == null)
        {
            throw new InvalidOperationException($"Usuário com Id {request.Id} não encontrado.");
        }

        await _usuarioRepository.DeleteAsync(request.Id);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
