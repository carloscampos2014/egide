using Egide.Domain.Entities;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class LicencaPorInstalacaoTest
{
    [Theory]
    [MemberData(nameof(GenerateLicenca))]
    public void Validar_Deve_RetornarSeValidouInvalida(bool status, int maxInstalacao, ValidationContext context, bool expected)
    {
        // Arrage
        var licenca = new LicencaPorInstalacao(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            maximoInstalacoes: maxInstalacao);
        licenca.Ativa.Should().BeTrue();

        // Act
        licenca.ModificarStatus(status);
        bool actual = licenca.Validar(context);

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
        var licenca = new LicencaPorInstalacao(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            maximoInstalacoes: 1);

        // Act
        licenca.ModificarStatus(status);

        // Asserts
        licenca.Ativa.Should().Be(status);
    }

    public static IEnumerable<object[]> GenerateLicenca()
    {
        yield return new object[] { true, 5, new ValidationContext() { ContagemInstalacoesAtuais = 4 }, true };
        yield return new object[] { true, 5, new ValidationContext() { ContagemInstalacoesAtuais = 5 }, true };
        yield return new object[] { true, 5, new ValidationContext() { ContagemInstalacoesAtuais = 6 }, false };
        yield return new object[] { true, 5, null, false };
        yield return new object[] { false, 5, new ValidationContext() { ContagemInstalacoesAtuais = 4 }, false };
        yield return new object[] { false, 5, new ValidationContext() { ContagemInstalacoesAtuais = 5 }, false };
        yield return new object[] { false, 5, new ValidationContext() { ContagemInstalacoesAtuais = 6 }, false };
        yield return new object[] { false, 5, null, false };
    }
}
