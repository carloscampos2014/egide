using Egide.Domain.Entities;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class LicencaPorTempoTest
{
    [Theory]
    [MemberData(nameof(GenerateLicenca))]
    public void Validar_Deve_RetornarSeValidouInvalida(bool status, string expiracaoData, bool expected)
    {
        // Arrage
        var dataexpiracao = DateTime.Parse(expiracaoData);
        var licenca = new LicencaPorTempo(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            dataexpiracao);
        licenca.Ativa.Should().BeTrue();

        // Act
        licenca.ModificarStatus(status);
        bool actual = licenca.Validar(null);

        // Asserts
        licenca.Ativa.Should().Be(status);
        actual.Should().Be(expected);
    }


    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ModificarStatus_Deve_AlterarStatus(bool status)
    {
        // Arrage
        var licenca = new LicencaPorTempo(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            dataExpiracao: DateTime.UtcNow);

        // Act
        licenca.ModificarStatus(status);

        // Asserts
        licenca.Ativa.Should().Be(status);
    }

    public static IEnumerable<object[]> GenerateLicenca()
    {
        var hoje = DateTime.UtcNow;
        yield return new object[] { true, hoje.ToString("yyyy-MM-dd'T'HH:mm:ss"), true };
        yield return new object[] { true, hoje.AddDays(1).ToString("yyyy-MM-dd'T'HH:mm:ss"), true };
        yield return new object[] { true, hoje.AddDays(-1).ToString("yyyy-MM-dd'T'HH:mm:ss"), false };
        yield return new object[] { false, hoje.ToString("yyyy-MM-dd'T'HH:mm:ss"), false };
        yield return new object[] { false, hoje.AddDays(1).ToString("yyyy-MM-dd'T'HH:mm:ss"), false };
        yield return new object[] { false, hoje.AddDays(-1).ToString("yyyy-MM-dd'T'HH:mm:ss"), false };
    }
}
