namespace Egide.Application.Abstractions;
/// <summary>
/// Abstrai a lógica de hash e verificação de senhas.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Cria um hash a partir de uma senha.
    /// </summary>
    string Hash(string password);

    /// <summary>
    /// Verifica se a senha fornecida corresponde ao hash armazenado.
    /// </summary>
    bool Verificar(string hash, string password);
}