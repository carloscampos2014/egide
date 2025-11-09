using Egide.Domain.Entities;
using Egide.Domain.Enums;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class LicencaTest
{
    [Theory]
    [MemberData(nameof(GenerateLicenca))]
    public void Validar_Deve_RetornarSeValidouInvalida(Enums.TipoLicenca tipo, bool status, Licenca licencaArg, ValidationContext context, bool expected)
    {
        // Arrage
        var licenca = new Licenca(
            tipo: licencaArg.Tipo,
            clienteId: licencaArg.ClienteId,
            softwareId: licencaArg.SoftwareId,
            dataExpiracao: licencaArg.DataExpiracao,
            maximoUsuarios: licencaArg.MaximoUsuarios,
            maximoInstalacoes: licencaArg.MaximoInstalacoes);

        licenca.Ativa.Should().BeTrue();

        // Act
        licenca.ModificarStatus(status);
        bool actual = licenca.Validar(context);

        // Asserts
        licenca.Ativa.Should().Be(status);
        actual.Should().Be(expected);
    }

    [Fact]
    public void AtualizarDados_Deve_AlterarNomePersonalidadeEDocumento()
    {
        // Arrange
        var licenca = new Licenca(
            tipo: Enums.TipoLicenca.Vitalicia,
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            dataExpiracao: DateTime.UtcNow,
            maximoUsuarios: 5,
            maximoInstalacoes: 5);

        var novoTipo = Enums.TipoLicenca.PorTempo;
        var novoDataExpiracao = DateTime.UtcNow.AddDays(1);
        var novoMaxUsuarios = 6;
        var novoMaxInstalacoes = 6;

        // Act
        licenca.AtualizarDados(novoTipo, novoDataExpiracao, novoMaxInstalacoes, novoMaxUsuarios);

        // Assert
        licenca.Tipo.Should().Be(novoTipo);
        licenca.DataExpiracao.Should().Be(novoDataExpiracao);
        licenca.MaximoInstalacoes.Should().Be(novoMaxInstalacoes);
        licenca.MaximoUsuarios.Should().Be(novoMaxUsuarios);
    }

    [Fact]
    public void TextoValidacao_Deve_DeveRetornarTextoValidacao()
    {
        // Arrange
        var licenca = new Licenca(
            tipo: Enums.TipoLicenca.PorUsuario,
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            dataExpiracao: DateTime.UtcNow,
            maximoUsuarios: 1,
            maximoInstalacoes: 5);

        var context = new ValidationContext() { ContagemUsuariosAtuais = 4, ContagemInstalacoesAtuais = 4 };
        string expected = $"Você atingiu o numero máximo de usuários ativos -> {licenca.MaximoUsuarios:D4}";


        // Act
        string actual = licenca.TextoValidacao(context);

        // Assert
        actual.Should().Be(expected);
    }


    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ModificarStatus_Deve_AlterarStatus(bool status)
    {
        // Arrage
        var licenca = new Licenca(
            tipo: Enums.TipoLicenca.Vitalicia,
            clienteId: Guid.NewGuid(),
            softwareId: Guid.NewGuid(),
            dataExpiracao: DateTime.UtcNow,
            maximoUsuarios: 5,
            maximoInstalacoes: 5);

        // Act
        licenca.ModificarStatus(status);

        // Asserts
        licenca.Ativa.Should().Be(status);
    }

    public static IEnumerable<object[]> GenerateLicenca()
    {
        var hoje = DateTime.UtcNow;
        // Vitalicia
        var tipo = Enums.TipoLicenca.Vitalicia;
        var licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje, 5, 5);
        yield return new object[] { tipo, true, licenca, null, true };
        yield return new object[] { tipo, false, licenca, null, false };

        var context = new ValidationContext() { ContagemUsuariosAtuais = 4, ContagemInstalacoesAtuais = 4 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, true };
        yield return new object[] { tipo, false, licenca, null, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(-1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, true };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 5, ContagemInstalacoesAtuais = 5 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 6, ContagemInstalacoesAtuais = 6 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        // Por Tempo
        tipo = Enums.TipoLicenca.PorTempo;
        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje, 5, 5);
        yield return new object[] { tipo, true, licenca, null, true };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 4, ContagemInstalacoesAtuais = 4 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, true };
        yield return new object[] { tipo, false, licenca, null, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(-1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, false };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 5, ContagemInstalacoesAtuais = 5 };
        yield return new object[] { tipo, true, licenca, context, false };
        yield return new object[] { tipo, false, licenca, context, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 6, ContagemInstalacoesAtuais = 6 };
        yield return new object[] { tipo, true, licenca, context, false };
        yield return new object[] { tipo, false, licenca, context, false };

        // Por Usuários
        tipo = Enums.TipoLicenca.PorUsuario;
        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje, 5, 5);
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 4, ContagemInstalacoesAtuais = 4 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(-1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 5, ContagemInstalacoesAtuais = 5 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 6, ContagemInstalacoesAtuais = 6 };
        yield return new object[] { tipo, true, licenca, context, false };
        yield return new object[] { tipo, false, licenca, context, false };

        // Por Instalação
        tipo = Enums.TipoLicenca.PorInstalacao;
        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje, 5, 5);
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 4, ContagemInstalacoesAtuais = 4 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        licenca = new Licenca(Guid.NewGuid(), Guid.NewGuid(), tipo, hoje.AddDays(-1), 5, 5);
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };
        yield return new object[] { tipo, true, licenca, null, false };
        yield return new object[] { tipo, false, licenca, null, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 5, ContagemInstalacoesAtuais = 5 };
        yield return new object[] { tipo, true, licenca, context, true };
        yield return new object[] { tipo, false, licenca, context, false };

        context = new ValidationContext() { ContagemUsuariosAtuais = 6, ContagemInstalacoesAtuais = 6 };
        yield return new object[] { tipo, true, licenca, context, false };
        yield return new object[] { tipo, false, licenca, context, false };
    }
}
