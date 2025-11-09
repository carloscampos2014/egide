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

    public string TextoValidacao(ValidationContext contexto)
    {
        if (!this.Ativa)
        {
            return "Essa licença não esta mais ativa.";
        }

        if (Tipo == TipoLicenca.PorInstalacao && (contexto == null || !contexto.ContagemInstalacoesAtuais.HasValue))
        {
            return "Erro na chamada da validação faltou passar o contexto de Validação";
        }

        if (Tipo == TipoLicenca.PorUsuario && (contexto == null || !contexto.ContagemUsuariosAtuais.HasValue))
        {
            return "Erro na chamada da validação faltou passar o contexto de Validação";
        }

        return Tipo switch
        {
            TipoLicenca.PorTempo => $"Sua licença venceu no dia -> {DataExpiracao.Date:dd/MM/yyyy}",
            TipoLicenca.PorUsuario => $"Você atingiu o numero máximo de usuários ativos -> {MaximoUsuarios:D4}" ,
            TipoLicenca.PorInstalacao => $"Você atingiu o numero máximo de instalações -> {MaximoInstalacoes:D4}",
        };
    }

    public void ModificarStatus(bool status)
    {
        Ativa = status;
    }
}
