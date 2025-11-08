using CpfCnpjLibrary;
using Egide.Domain.Enums;
using FluentValidation;

namespace Egide.Application.UseCases.Clientes.Commands.Create;
/// <summary>
/// Validador para o CreateClienteCommand.
/// Garante que os dados de entrada cumprem as regras de negócio básicas.
/// </summary>
public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres.");

        RuleFor(x => x.Personalidade)
            .Must(p => Enum.IsDefined(typeof(Personalidade), p))
            .WithMessage("A personalidade informada não é válida. Deve ser 0 (Jurídico) ou 1 (Físico).");

        RuleFor(x => x.Documento)
            .NotEmpty().WithMessage("O documento é obrigatório.")
            .Must(doc => Cnpj.Validar(doc)) // 3. Use a nova sintaxe
            .WithMessage("O documento informado não é um CNPJ válido.")
            .When(x => x.Personalidade == Personalidade.Juridico); 

        RuleFor(x => x.Documento)
            .NotEmpty().WithMessage("O documento é obrigatório.")
            .Must(doc => Cpf.Validar(doc)) // 5. Use a nova sintaxe
            .WithMessage("O documento informado não é um CPF válido.")
            .When(x => x.Personalidade == Personalidade.Fisico);
    }
}
