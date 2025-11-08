using FluentValidation;

namespace Egide.Application.UseCases.Softwares.Commands.Update;
public class UpdateSoftwareCommadValidator : AbstractValidator<UpdateSoftwareCommad>
{
    public UpdateSoftwareCommadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O Id do software é obrigatório para atualização.");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("O titulo do software é obrigatório.")
            .Length(3, 200)
            .WithMessage("O titulo do software deve ter no mínimo 3 e no máximo 200 caracteres.");

        RuleFor(x => x.VersaoAtual)
            .NotEmpty().WithMessage("A versão atual do software é obrigatória.")
            .Matches(@"^\d+\.\d+\.\d+$")
            .WithMessage("O formato da versão do software deve ser XXX.XXX.XXX (ex: 1.0.0 ou 10.2.15).");
    }
}
