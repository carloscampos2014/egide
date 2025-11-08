namespace Egide.Domain.Entities;
public class Software
{
    public Software(string titulo, string descricao, string versaoAtual)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        VersaoAtual = versaoAtual;
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }

    private Software()
    {
        
    }

    public Guid Id { get; private set; }

    public string Titulo { get; private set; }

    public string Descricao { get; private set; }

    public string VersaoAtual { get; private set; }

    public bool Ativo { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public void AtualizarDados(string titulo, string descricao, string versaoAtual)
    {
        Titulo = titulo;
        Descricao = descricao;
        VersaoAtual = versaoAtual;
    }

    public void ModificarStatus(bool status)
    {
        Ativo = status;
    }
}
