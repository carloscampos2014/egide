namespace Egide.Domain.Entities;

public class Usuario
{
    private Usuario(Guid id, string nome, string email, string hashSenha, string tokenTenant, bool estaAtivo, DateTime dataCriacao, DateTime dataAtualizacao)
    {
        Id = id;
        Nome = nome;
        Email = email;
        HashSenha = hashSenha;
        TokenTenant = tokenTenant;
        EstaAtivo = estaAtivo;
        DataCriacao = dataCriacao;
        DataAtualizacao = dataAtualizacao;
    }

    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public string Email { get; private set; }

    public string HashSenha { get; private set; }

    public string TokenTenant { get; private set; }

    public bool EstaAtivo { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public DateTime DataAtualizacao { get; private set; }

    public static Usuario Criar(string nome, string email, string hashSenha)
    {
        var id = Guid.NewGuid();
        var tokenTenant = string.Empty;
        var estaAtivo = true;
        var agora = DateTime.UtcNow;

        return new Usuario(id, nome, email, hashSenha, tokenTenant, estaAtivo, agora, agora);
    }

    public void AtualizarNome(string novoNome)
    {
        if (!string.IsNullOrWhiteSpace(novoNome) && novoNome != Nome)
        {
            Nome = novoNome;
            DataAtualizacao = DateTime.UtcNow;
        }
    }

    public void DefinirSenha(string novoHashSenha)
    {
        if (!string.IsNullOrWhiteSpace(novoHashSenha))
        {
            HashSenha = novoHashSenha;
            DataAtualizacao = DateTime.UtcNow;
        }
    }

    public void DefinirTokenTenant(string tokenTenant)
    {
        TokenTenant = tokenTenant ?? string.Empty;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void DefinirAtivo(bool ativo)
    {
        EstaAtivo = ativo;
        DataAtualizacao = DateTime.UtcNow;
    }
}
