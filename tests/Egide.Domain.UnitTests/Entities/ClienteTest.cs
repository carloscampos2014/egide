using Egide.Domain.Entities;
using Egide.Domain.Enums;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class ClienteTests
{
    [Fact] // Define isto como um método de teste xUnit
    public void AtualizarDados_Deve_AlterarNomePersonalidadeEDocumento()
    {
        // --- Arrange (Organizar) ---
        // Criamos o estado inicial do nosso objeto
        var cliente = new Cliente(
            "Nome Antigo",
            Personalidade.Fisico,
            "12345678900"
        );

        var novoNome = "Nome Novo";
        var novaPersonalidade = Personalidade.Juridico;
        var novoDocumento = "12345678000199";

        // --- Act (Agir) ---
        // Executamos o método que queremos testar
        cliente.AtualizarDados(novoNome, novaPersonalidade, novoDocumento);

        // --- Assert (Verificar) ---
        // Usamos o FluentAssertions para verificar se o estado mudou como esperado
        cliente.Nome.Should().Be(novoNome);
        cliente.Personalidade.Should().Be(novaPersonalidade);
        cliente.Documento.Should().Be(novoDocumento);
    }

    [Fact]
    public void ModificarStatus_Deve_AlterarPropriedadeAtivo()
    {
        // Arrange
        var cliente = new Cliente("Teste", Personalidade.Fisico, "12345678900");
        cliente.Ativo.Should().BeTrue(); // Garante o estado inicial

        // Act
        cliente.ModificarStatus(false);

        // Assert
        cliente.Ativo.Should().BeFalse();
    }
}