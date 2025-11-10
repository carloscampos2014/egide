using Egide.Application.Abstractions;
using Egide.Application.Abstractions.Authentication;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces.Usuarios;
using MediatR;

namespace Egide.Application.UseCases.Usuarios.Queries.Login;
public class LoginUsuarioQueryHandler(IUsuarioReadRepository readRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginUsuarioQuery, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginUsuarioQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await readRepository.ObterPorEmailAsync(request.Email) ?? throw new InvalidOperationException("Login/senha invalido.");
            if (!passwordHasher.Verificar(usuario.HashSenha, request.Senha))
            {
                throw new InvalidOperationException("Login/senha invalido.");
            }

            if (!usuario.EstaAtivo)
            {
                throw new InvalidOperationException("Este usuário não esta ativo.");
            }

            var token = jwtTokenGenerator.GerarToken(usuario, false);

            return new LoginResponse(
                sucesso: true,
                mensagemErro: "",
                id: usuario.Id,
                nome: usuario.Nome,
                tokenSecao: token,
                tokenTenat: usuario.TokenTenant);
        }
        catch (Exception ex)
        {
            return new LoginResponse(
                sucesso: false,
                mensagemErro: ex.Message,
                id: Guid.NewGuid());
        }
    }
}
