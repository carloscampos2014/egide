using Egide.Domain.Enums;

namespace Egide.Domain.Entities;
public class Cliente
{
    public Cliente(string nome, Personalidade personalidade, string documento)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Personalidade = personalidade;
        Documento = documento;
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public Personalidade Personalidade { get; private set; }

    public string Documento { get; private set; }

    public bool Ativo { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public void AtualizarDados(string nome, Personalidade personalidade, string documento)
    {
        Nome = nome;
        Personalidade = personalidade;
        Documento = documento;
    }

    public void ModificarStatus(bool status)
    {
        Ativo = status;
    }
}
