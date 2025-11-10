using Egide.Domain.Entities;

namespace Egide.Application.Abstractions.Authentication;

/// <summary>
/// Define o contrato para um serviço que gera tokens JWT.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Gera um token JWT para o usuário especificado.
    /// </summary>
    string GerarToken(Usuario usuario, bool longaDuracao = false);
}