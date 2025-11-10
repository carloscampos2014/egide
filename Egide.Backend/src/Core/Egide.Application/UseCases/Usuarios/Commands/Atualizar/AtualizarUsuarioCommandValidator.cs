using FluentValidation;

namespace Egide.Application.UseCases.Usuarios.Commands.Atualizar;

public class AtualizarUsuarioCommandValidator : AbstractValidator<AtualizarUsuarioCommand>
{
    public AtualizarUsuarioCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O Id do usuário é obrigatório para atualização.");

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome do usuário é obrigatório.")
            .Length(3, 200)
            .WithMessage("O nome do usuário deve ter no mínimo 3 e no máximo 200 caracteres.");

        When(x => !string.IsNullOrEmpty(x.SenhaNova), () =>
        {
            RuleFor(x => x.SenhaAtual)
                .NotEmpty().WithMessage("A senha atual é obrigatória para definir uma nova senha.");

            RuleFor(x => x.SenhaNova)
                .MinimumLength(8).WithMessage("A nova senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A nova senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A nova senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A nova senha deve conter pelo menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A nova senha deve conter pelo menos um símbolo.");

            RuleFor(x => x.ConfirmarSenhaNova)
                .Equal(x => x.SenhaNova)
                .WithMessage("As senhas (nova senha e confirmação) não coincidem.");
        });
    }
}