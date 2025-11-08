using FluentValidation;

namespace Egide.Application.UseCases.Softwares.Commands.Create;
public class CreateSoftwareCommandValidator : AbstractValidator<CreateSoftwareCommand>
{
    public CreateSoftwareCommandValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("O titulo é obrigatório.")
            .Length(3, 200)
            .WithMessage("O titulo deve ter no mínimo 3 e no máximo 200 caracteres.");

        RuleFor(x => x.VersaoAtual)
            .NotEmpty().WithMessage("A versão atual é obrigatória.")
            .Matches(@"^\d+\.\d+\.\d+$")
            .WithMessage("O formato da versão deve ser XXX.XXX.XXX (ex: 1.0.0 ou 10.2.15).");
    }
}
}
