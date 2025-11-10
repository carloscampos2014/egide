using Egide.Application.Abstractions;
using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Atualizar;

public class AtualizarUsuarioCommandHandler(
    IUsuarioReadRepository readRepository,
    IUsuarioWriteRepository writeRepository,
    IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : IRequestHandler<AtualizarUsuarioCommand, Unit>
{
    public async Task<Unit> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await readRepository.ObterPorIdAsync(request.Id) ?? throw new InvalidOperationException($"Usuário com Id {request.Id} não encontrado.");
        usuario.AtualizarNome(request.Nome);
        if (!string.IsNullOrWhiteSpace(request.SenhaNova))
        {
            if (!passwordHasher.Verificar(usuario.HashSenha, request.SenhaAtual))
            {
                throw new InvalidOperationException("A senha atual está incorreta.");
            }

            var novoHash = passwordHasher.Hash(request.SenhaNova);
            usuario.DefinirSenha(novoHash);
        }

        await writeRepository.AtualizarAsync(usuario);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}