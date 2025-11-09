using Egide.Domain.Enums;
using FluentValidation;

namespace Egide.Application.UseCases.Licencas.Commands.Update;
public class UpdateLicencaCommandValidator : AbstractValidator<UpdateLicencaCommand>
{
    public UpdateLicencaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O Id da licença é obrigatório para atualização.");

        RuleFor(x => x.Tipo)
            .Must(p => Enum.IsDefined(typeof(TipoLicenca), p))
            .WithMessage("O tipo de licença informado não é válida. Deve ser 0 (Vitalicia) ou 1 (Por Tempo) 2 (Por Número de Usuários) 3 (Por Número de Instalações).");

        RuleFor(x => x.DataExpiracao)
            .NotEmpty()
            .WithMessage("A Data de Expiração é obrigatória.")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("A Data de Expiração deve ser uma data futura.")
            .When(x => x.Tipo == TipoLicenca.PorTempo);

        RuleFor(x => x.MaximoUsuarios)
            .NotEmpty()
            .WithMessage("O Número Máximo de Usuários é obrigatório.")
            .GreaterThan(0)
            .WithMessage("O Número Máximo de Usuários deve ser maior que 0.")
            .When(x => x.Tipo == TipoLicenca.PorUsuario);

        RuleFor(x => x.MaximoInstalacoes)
            .NotEmpty()
            .WithMessage("O Número Máximo de Instalações é obrigatório.")
            .GreaterThan(0)
            .WithMessage("O Número Máximo de Instalações deve ser maior que 0.")
            .When(x => x.Tipo == TipoLicenca.PorUsuario);
    }
}
