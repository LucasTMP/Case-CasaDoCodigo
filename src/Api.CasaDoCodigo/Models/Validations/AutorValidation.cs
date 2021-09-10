using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace Api.CasaDoCodigo.Models.Validations
{
    public class AutorValidation : AbstractValidator<Autor>
    {

        public AutorValidation()
        {
            RuleFor(parameter => parameter.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");


            RuleFor(parameter => parameter.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("O {PropertyName} cadastrado precisa ser válido.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 400).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.CreatedAt)
                .NotEmpty().WithMessage("O campo data de criação não pode estar em branco.")
                .NotNull().WithMessage("O campo data de criação não pode ser nulo.")
                .Must(date => date != default(DateTime)).WithMessage("A data de criação precisa ser válida.")
                .LessThanOrEqualTo(p => DateTime.Now).WithMessage("A data de criação deve ser a presente.");

            RuleFor(parameter => parameter.UpdatedAt)
                .NotEmpty().WithMessage("O campo data de alteração não pode estar em branco.")
                .NotNull().WithMessage("O campo data de alteração não pode ser nulo.")
                .Must(date => date != default(DateTime)).WithMessage("A data de alteração precisa ser válida.")
                .GreaterThanOrEqualTo(p => p.CreatedAt).WithMessage("A data de alteração deve ser maior que a data de criação.");
        }

    }
}
