using Egide.Application.Abstractions;

namespace Egide.Infrastructure.Security;
public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        // O BCrypt gera o salt automaticamente
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verificar(string hash, string password)
    {
        // O BCrypt extrai o salt do hash e faz a comparação
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}