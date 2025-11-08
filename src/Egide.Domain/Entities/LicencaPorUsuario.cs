
namespace Egide.Domain.Entities;
public class LicencaPorUsuario : Licenca
{
    private LicencaPorUsuario()
    {
    }

    public LicencaPorUsuario(Guid clienteId, Guid softwareId, int maximoUsuarios) : base(clienteId, softwareId)
    {
        MaximoUsuarios = maximoUsuarios;
    }

    public int MaximoUsuarios { get; private set; }

    protected override bool RegraEspecifica(ValidationContext contexto)
    {
        if (contexto == null ||  !contexto.ContagemUsuariosAtuais.HasValue)
        {
            return false;
        }

        return contexto.ContagemUsuariosAtuais <= MaximoUsuarios;
    }
}
