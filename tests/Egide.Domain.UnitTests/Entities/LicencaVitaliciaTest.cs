using Egide.Domain.Entities;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class LicencaVitaliciaTest
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Validar_Deve_RetornarSeValidouInvalida(bool status)
    {
        // Arrage
        var licenca = new LicencaVitalicia(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid());
        licenca.Ativa.Should().BeTrue();

        // Act
        licenca.ModificarStatus(status);
        bool actual = licenca.Validar(null);

        // Asserts
        licenca.Ativa.Should().Be(status);
        actual.Should().Be(status);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ModificarStatus_Deve_AlterarStatus(bool status)
    {
        // Arrage
        var licenca = new LicencaVitalicia(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid());

        // Act
        licenca.ModificarStatus(status);

        // Asserts
        licenca.Ativa.Should().Be(status);
    }
}
