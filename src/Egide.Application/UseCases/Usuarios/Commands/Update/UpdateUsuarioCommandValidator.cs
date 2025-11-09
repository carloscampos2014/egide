using FluentValidation;

namespace Egide.Application.UseCases.Usuarios.Commands.Update;
public class UpdateUsuarioCommandValidator : AbstractValidator<UpdateUsuarioCommand>
{
    public UpdateUsuarioCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O Id do usuário é obrigatório para atualização.");

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome do usuário é obrigatório.")
            .Length(3, 200)
            .WithMessage("O nome do usuário deve ter no mínimo 3 e no máximo 200 caracteres.");

        When(x => !string.IsNullOrEmpty(x.NovaSenha), () =>
        {
            // 1. Se preencheu a nova senha, DEVE preencher a senha atual
            RuleFor(x => x.SenhaAtual)
                .NotEmpty().WithMessage("A senha atual é obrigatória para definir uma nova senha.");

            // 2. Aplica as regras de complexidade à nova senha
            RuleFor(x => x.NovaSenha)
                .MinimumLength(8).WithMessage("A nova senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A nova senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A nova senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A nova senha deve conter pelo menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A nova senha deve conter pelo menos um símbolo.");

            // 3. Aplica a regra de confirmação
            RuleFor(x => x.ConfirmarSenha)
                .Equal(x => x.NovaSenha)
                .WithMessage("As senhas (nova senha e confirmação) não coincidem.");
        });
    }
}
