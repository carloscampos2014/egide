namespace Egide.Domain.Entities;
public abstract class Licenca
{

    protected Licenca()
    {

    }

    protected Licenca(Guid clienteId, Guid softwareId)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        SoftwareId = softwareId;
        Ativa = true;
        DataCriacao = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid ClienteId { get; private set; }

    public Guid SoftwareId { get; private set; }

    public bool Ativa { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public bool Validar(ValidationContext contexto)
    {
        if (!this.Ativa)
        {
            return false;
        }

        return RegraEspecifica(contexto);
    }

    protected abstract bool RegraEspecifica(ValidationContext contexto);


    public void ModificarStatus(bool status)
    {
        Ativa = status;
    }
}
