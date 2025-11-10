using FluentValidation;

namespace Egide.Application.UseCases.Usuarios.Queries.Login;
public class LoginUsuarioQueryValidatorValidator : AbstractValidator<LoginUsuarioQuery>
{
    public LoginUsuarioQueryValidatorValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail fornecido não é válido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}
