using Egide.Domain.Enums;

namespace Egide.Domain.Entities;
public class Licenca
{

    private Licenca()
    {

    }

    public Licenca(Guid clienteId, Guid softwareId, TipoLicenca tipo, DateTime dataExpiracao, int maximoInstalacoes, int maximoUsuarios)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        SoftwareId = softwareId;
        Tipo = tipo;
        DataExpiracao = dataExpiracao;
        MaximoInstalacoes = maximoInstalacoes;
        MaximoUsuarios = maximoUsuarios;
        Ativa = true;
        DataCriacao = DateTime.UtcNow;
    }

    public Guid Id { get;  private set; }

    public Guid ClienteId { get; private set; }

    public Guid SoftwareId { get; private set; }

    public TipoLicenca Tipo { get; set; } = TipoLicenca.Vitalicia;

    public DateTime DataExpiracao { get; private set; } = DateTime.UtcNow;

    public int MaximoInstalacoes { get; private set; }

    public int MaximoUsuarios { get; private set; }

    public bool Ativa { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public void AtualizarDados(TipoLicenca tipo, DateTime dataExpiracao, int maximoInstalacoes, int maximoUsuarios)
    {
        Tipo = tipo;
        DataExpiracao = dataExpiracao;
        MaximoInstalacoes = maximoInstalacoes;
        MaximoUsuarios = maximoUsuarios;
    }

    public bool Validar(ValidationContext contexto)
    {
        if (!this.Ativa)
        {
            return false;
        }

        if ((Tipo == TipoLicenca.PorUsuario || Tipo == TipoLicenca.PorInstalacao ) && (contexto == null || !contexto.ContagemInstalacoesAtuais.HasValue))
        {
            return false;
        }

        return Tipo switch
        {
            TipoLicenca.Vitalicia => true,
            TipoLicenca.PorTempo => DateTime.UtcNow.Date <= DataExpiracao.Date,
            TipoLicenca.PorUsuario => contexto.ContagemUsuariosAtuais <= MaximoUsuarios,
            TipoLicenca.PorInstalacao => contexto.ContagemInstalacoesAtuais <= MaximoInstalacoes,
        };
    }

    public void ModificarStatus(bool status)
    {
        Ativa = status;
    }
}
