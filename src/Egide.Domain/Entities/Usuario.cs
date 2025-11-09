using Egide.Domain.Enums;

namespace Egide.Domain.Entities;
public class Usuario
{
    private Usuario()
    {
        
    }

    public Usuario(string nome, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        PasswordHash = passwordHash;
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public string TenenteToken { get; private set; }

    public bool Ativo { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public void AtualizarDados(string nome, string passwordHash)
    {
        Nome = nome;
        PasswordHash = passwordHash;
    }

    public void AtualizarTenenteToken(string tenenteToken)
    {
        TenenteToken = tenenteToken;
    }

    public void ModificarStatus(bool status)
    {
        Ativo = status;
    }
}
