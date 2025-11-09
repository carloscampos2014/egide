namespace Egide.Domain.Entities;
public class ResultadoValidacao
{
    public ResultadoValidacao(bool valida, string mensagemValidacao)
    {
        Valida = valida;
        MensagemValidacao = mensagemValidacao;
    }

    public bool Valida { get; private set; }

    public string MensagemValidacao { get; private set; }
}
