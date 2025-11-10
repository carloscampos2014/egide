using Egide.Application.Abstractions;
using Egide.Application.Abstractions.Authentication;
using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Commands.Criar;

public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, Guid>
{
    private readonly IUsuarioReadRepository _readRepository;
    private readonly IUsuarioWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public CriarUsuarioCommandHandler(IUsuarioReadRepository readRepository, IUsuarioWriteRepository writeRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Guid> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var emailJaExiste = await _readRepository.ObterPorEmailAsync(request.Email);
        if (emailJaExiste is not null)
        {
            throw new InvalidOperationException("O endereço de e-mail já está em uso."); // Idealmente, uma exceção customizada.
        }

        var hashSenha = _passwordHasher.Hash(request.Senha);

        var usuario = Domain.Entities.Usuario.Criar(request.Nome, request.Email, hashSenha);
        var token = _jwtTokenGenerator.GerarToken(usuario, true);
        usuario.DefinirTokenTenant(token);

        await _writeRepository.AdicionarAsync(usuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return usuario.Id;
    }
}