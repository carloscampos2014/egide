using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Update;
public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Unit>
{
    private IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
        if (usuario == null)
        {
            throw new InvalidOperationException($"Usuário com Id {request.Id} não encontrado.");
        }

        usuario.AtualizarDados(request.Nome, request.Email, request.PasswordHash);

        await _usuarioRepository.UpdateAsync(usuario);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
