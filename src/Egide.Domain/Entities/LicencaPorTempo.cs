namespace Egide.Domain.Entities;
public class LicencaPorTempo : Licenca
{
    public LicencaPorTempo()
    {
    }

    public LicencaPorTempo(Guid clienteId, Guid softwareId, DateTime dataExpiracao) : base(clienteId, softwareId)
    {
        DataExpiracao = dataExpiracao;
    }

    public DateTime DataExpiracao { get; private set; }

    protected override bool RegraEspecifica(ValidationContext contexto)
    {
        return DateTime.UtcNow.Date <= DataExpiracao.Date;
    }
}
