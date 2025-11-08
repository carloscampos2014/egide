using Egide.Domain.Entities;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class LicencaPorUsuarioTest
{
    [Theory]
    [MemberData(nameof(GenerateLicenca))]
    public void Validar_Deve_RetornarSeValidouInvalida(bool status, int maxusuarios, ValidationContext context, bool expected)
    {
        // Arrage
        var licenca = new LicencaPorUsuario(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            maximoUsuarios: maxusuarios);
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
        var licenca = new LicencaPorUsuario(
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            maximoUsuarios: 1);

        // Act
        licenca.ModificarStatus(status);

        // Asserts
        licenca.Ativa.Should().Be(status);
    }

    public static IEnumerable<object[]> GenerateLicenca()
    {
        yield return new object[] { true, 5, new ValidationContext() { ContagemUsuariosAtuais = 4 }, true };
        yield return new object[] { true, 5, new ValidationContext() { ContagemUsuariosAtuais = 5 }, true };
        yield return new object[] { true, 5, new ValidationContext() { ContagemUsuariosAtuais = 6 }, false };
        yield return new object[] { true, 5, null, false };
        yield return new object[] { false, 5, new ValidationContext() { ContagemUsuariosAtuais = 4 }, false };
        yield return new object[] { false, 5, new ValidationContext() { ContagemUsuariosAtuais = 5 }, false };
        yield return new object[] { false, 5, new ValidationContext() { ContagemUsuariosAtuais = 6 }, false };
        yield return new object[] { false, 5, null, false };
    }
}
