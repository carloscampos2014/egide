using Egide.Application.Abstractions;

namespace Egide.Infrastructure.Security;
public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verificar(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}