namespace Egide.Infrastructure.Security;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    /// <summary>
    /// A chave secreta para assinar o token. Deve ser longa e complexa.
    /// </summary>
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// O emissor (issuer) do token.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// A audiência (audience) do token.
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    public int ExpiryMinutes { get; init; }

    public int LongExpiryDays { get; init; }
}