namespace Egide.Domain.Entities;
public class LicencaVitalicia : Licenca
{
    private LicencaVitalicia()
    {
    }

    public LicencaVitalicia(Guid clienteId, Guid softwareId) : base(clienteId, softwareId)
    {
    }

    protected override bool RegraEspecifica(ValidationContext contexto)
    {
        return true;
    }
}
