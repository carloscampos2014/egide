
namespace Egide.Domain.Entities;
public class LicencaPorInstalacao :Licenca
{
    private LicencaPorInstalacao()
    {
    }

    public LicencaPorInstalacao(Guid clienteId, Guid softwareId, int maximoInstalacoes) : base(clienteId, softwareId)
    {
        MaximoInstalacoes = maximoInstalacoes;
    }

    public int MaximoInstalacoes { get; private set; }

    protected override bool RegraEspecifica(ValidationContext contexto)
    {
        if (contexto == null || !contexto.ContagemInstalacoesAtuais.HasValue)
        {
            return false;
        }

        return contexto.ContagemInstalacoesAtuais <= MaximoInstalacoes;
    }
}
