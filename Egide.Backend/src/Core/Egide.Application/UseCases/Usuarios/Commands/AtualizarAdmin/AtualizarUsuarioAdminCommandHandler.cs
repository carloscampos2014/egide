using Egide.Application.Abstractions;
using Egide.Application.Abstractions.Authentication;
using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.AtualizarAdmin;

public class AtualizarUsuarioAdminCommandHandler(
    IUsuarioReadRepository readRepository,
    IUsuarioWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<AtualizarUsuarioAdminCommand, Unit>
{
    public async Task<Unit> Handle(AtualizarUsuarioAdminCommand request, CancellationToken cancellationToken)
    {
        var usuario = await readRepository.ObterPorIdAsync(request.Id) ?? throw new InvalidOperationException($"Usuário com Id {request.Id} não encontrado.");
        usuario.AtualizarNome(request.Nome);
        var novoHash = passwordHasher.Hash(request.Senha);
        usuario.DefinirSenha(novoHash);
        usuario.DefinirAtivo(request.EstaAtivo);
        if (request.Acao == Domain.Enums.AcaoTokenTenat.Renovar)
        {
            usuario.DefinirTokenTenant(string.Empty);
        }
        else
        {
            var token = jwtTokenGenerator.GerarToken(usuario, true);
            usuario.DefinirTokenTenant(token);
        }

        await writeRepository.AtualizarAsync(usuario);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}