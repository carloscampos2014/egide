using Bogus;
using Egide.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Egide.Domain.Tests.Entities;

public class UsuarioTest
{
    private readonly Faker _faker;

    public UsuarioTest()
    {
        // Usamos um Faker para gerar dados de teste consistentes e realistas.
        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "Deve criar um usuário válido com dados corretos")]
    [Trait("Category", "Domain - Usuario")]
    public void Criar_Deve_RetornarUsuarioValido_QuandoDadosCorretos()
    {
        // Arrange
        var nome = _faker.Person.FullName;
        var email = _faker.Person.Email;
        var senhaHash = _faker.Random.Hash();

        // Act
        var usuario = Usuario.Criar(nome, email, senhaHash);

        // Assert
        usuario.Should().NotBeNull();
        usuario.Nome.Should().Be(nome);
        usuario.Email.Should().Be(email);
        usuario.HashSenha.Should().Be(senhaHash);
        usuario.Id.Should().NotBeEmpty();
        usuario.EstaAtivo.Should().BeTrue();
        usuario.DataCriacao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        usuario.DataAtualizacao.Should().Be(usuario.DataCriacao);
        usuario.TokenTenant.Should().BeNullOrEmpty();
    }

    [Fact(DisplayName = "Deve definir o token do tenant corretamente")]
    [Trait("Category", "Domain - Usuario")]
    public void DefinirTokenTenant_Deve_AtualizarTokenCorretamente()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        var token = _faker.Random.AlphaNumeric(128);

        // Act
        Thread.Sleep(10);
        usuario.DefinirTokenTenant(token);

        // Assert
        usuario.TokenTenant.Should().Be(token);
        usuario.DataAtualizacao.Should().BeAfter(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Deve definir o token Empty do tenant corretamente")]
    [Trait("Category", "Domain - Usuario")]
    public void DefinirTokenTenant_DeveTokenEmpty_AtualizarTokenCorretamente()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        var token = string.Empty;

        // Act
        Thread.Sleep(10);
        usuario.DefinirTokenTenant(token);

        // Assert
        usuario.TokenTenant.Should().Be(token);
        usuario.DataAtualizacao.Should().BeAfter(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Deve definir o token Null do tenant corretamente")]
    [Trait("Category", "Domain - Usuario")]
    public void DefinirTokenTenant_DeveTokenNull_AtualizarTokenCorretamente()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;

        // Act
        Thread.Sleep(10);
        usuario.DefinirTokenTenant(null);

        // Assert
        usuario.TokenTenant.Should().Be(string.Empty);
        usuario.DataAtualizacao.Should().BeAfter(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Deve atualizar o nome e a data de atualização")]
    [Trait("Category", "Domain - Usuario")]
    public void AtualizarNome_Deve_AlterarNomeEDataAtualizacao()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        var novoNome = _faker.Person.UserName;

        // Act
        // Pequeno delay para garantir que a data de atualização será diferente
        Thread.Sleep(10);
        usuario.AtualizarNome(novoNome);

        // Assert
        usuario.Nome.Should().Be(novoNome);
        usuario.DataAtualizacao.Should().BeAfter(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Não Deve atualizar o nome e a data de atualização")]
    [Trait("Category", "Domain - Usuario")]
    public void AtualizarNome_NaoDeve_AlterarNomeEDataAtualizacao()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        var novoNome = _faker.Person.FullName;

        // Act
        // Pequeno delay para garantir que a data de atualização será diferente
        Thread.Sleep(10);
        usuario.AtualizarNome(novoNome);

        // Assert
        usuario.Nome.Should().Be(novoNome);
        usuario.DataAtualizacao.Should().Be(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Não Deve atualizar o nome em branco e a data de atualização")]
    [Trait("Category", "Domain - Usuario")]
    public void AtualizarNome_NaoDeveNomeEmpty_AlterarNomeEDataAtualizacao()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        var novoNome = string.Empty;

        // Act
        // Pequeno delay para garantir que a data de atualização será diferente
        Thread.Sleep(10);
        usuario.AtualizarNome(novoNome);

        // Assert
        usuario.Nome.Should().NotBe(novoNome);
        usuario.DataAtualizacao.Should().Be(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Deve definir nova senha e atualizar data de atualização")]
    [Trait("Category", "Domain - Usuario")]
    public void DefinirSenha_Deve_AlterarSenhaHashEDataAtualizacao()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        var novoHash = _faker.Random.Hash(64);

        // Act
        Thread.Sleep(10);
        usuario.DefinirSenha(novoHash);

        // Assert
        usuario.HashSenha.Should().Be(novoHash);
        usuario.DataAtualizacao.Should().BeAfter(dataAtualizacaoAntiga);
    }

    [Fact(DisplayName = "Deve inativar um usuário ativo")]
    [Trait("Category", "Domain - Usuario")]
    public void Inativar_Deve_TornarUsuarioInativoEAtualizarData()
    {
        // Arrange
        var usuario = Usuario.Criar(_faker.Person.FullName, _faker.Person.Email, _faker.Random.Hash());
        var dataAtualizacaoAntiga = usuario.DataAtualizacao;
        
        // Garante que o estado inicial é o esperado
        usuario.EstaAtivo.Should().BeTrue();

        // Act
        Thread.Sleep(10);
        usuario.DefinirAtivo(false);

        // Assert
        usuario.EstaAtivo.Should().BeFalse();
        usuario.DataAtualizacao.Should().BeAfter(dataAtualizacaoAntiga);
    }
}
