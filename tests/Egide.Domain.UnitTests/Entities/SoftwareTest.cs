using Egide.Domain.Entities;
using FluentAssertions;

namespace Egide.Domain.UnitTests.Entities;
public class SoftwareTest
{
    [Fact] // Define isto como um método de teste xUnit
    public void AtualizarDados_Deve_AlterarNomePersonalidadeEDocumento()
    {
        // --- Arrange (Organizar) ---
        // Criamos o estado inicial do nosso objeto
        var software = new Software(
            "Titulo Antigo",
            "Descrição Antiga",
            "1.0.0"
        );

        var novoTitulo = "Titulo Novo";
        var novaDescricao = "Descrição Nova ";
        var novaVersaoAtual = "1.0.1";

        // --- Act (Agir) ---
        // Executamos o método que queremos testar
        software.AtualizarDados(novoTitulo, novaDescricao, novaVersaoAtual);

        // --- Assert (Verificar) ---
        // Usamos o FluentAssertions para verificar se o estado mudou como esperado
        software.Titulo.Should().Be(novoTitulo);
        software.Descricao.Should().Be(novaDescricao);
        software.VersaoAtual.Should().Be(novaVersaoAtual);
    }

    [Fact]
    public void ModificarStatus_Deve_AlterarPropriedadeAtivo()
    {
        // Arrange
        var software = new Software("Teste", "Teste", "1.0.0");
        software.Ativo.Should().BeTrue(); // Garante o estado inicial

        // Act
        software.ModificarStatus(false);

        // Assert
        software.Ativo.Should().BeFalse();
    }
}
