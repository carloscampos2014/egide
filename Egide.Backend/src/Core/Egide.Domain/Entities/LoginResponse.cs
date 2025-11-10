namespace Egide.Domain.Entities;
public class LoginResponse(bool sucesso, string mensagemErro, Guid id, string nome = "", string tokenSecao = "", string tokenTenat = "")
{
    public bool Sucesso { get; } = sucesso;

    public string MensagemErro { get; } = mensagemErro;

    public Guid Id { get; } = id;

    public string Nome { get; } = nome;

    public string TokenSecao { get; } = tokenSecao;

    public string TokenTenat { get; } = tokenTenat;
}
