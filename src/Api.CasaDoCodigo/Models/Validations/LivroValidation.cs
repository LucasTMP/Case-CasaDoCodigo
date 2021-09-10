using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.Validations
{
    public class LivroValidation : AbstractValidator<Livro>
    {

        public LivroValidation()
        {
            RuleFor(parameter => parameter.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 2083).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.")
                .Must(o => !(Uri.IsWellFormedUriString(o, UriKind.Absolute))).WithMessage("O endereço da imagem é inválido.");

            RuleFor(parameter => parameter.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.SubTitulo)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Resumo)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 500).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Sumario)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.");

            RuleFor(parameter => parameter.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .GreaterThanOrEqualTo(20).WithMessage("O {PropertyName} tem que ser maior que {ComparisonValue}.");

            RuleFor(parameter => parameter.TotalDePaginas)
                .NotEmpty().WithMessage("O campo total de paginas não pode ser vazio.")
                .NotNull().WithMessage("O campo total de paginas não pode ser nulo.")
                .GreaterThanOrEqualTo(100).WithMessage("O total de paginas tem que ser maior que {ComparisonValue}.");

            RuleFor(parameter => parameter.ISBN)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(10, 13).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} numeros.");

            RuleFor(parameter => parameter.DataPublicacao)
                .NotEmpty().WithMessage("O campo data de publicação não pode estar em branco.")
                .NotNull().WithMessage("O campo data de publicação não pode ser nulo.")
                .Must(date => date != default(DateTime)).WithMessage("A data de publicação precisa ser válida.")
                .GreaterThanOrEqualTo(p => p.CreatedAt).WithMessage("A data de publicação deve ser maior que a data de criação.");

            RuleFor(parameter => parameter.CategoriaId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Must(o => o.ToString().Length == 36).WithMessage("O campo {PropertyName} tem que possuir 36 digitos.");

            RuleFor(parameter => parameter.AutorId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Must(o => o.ToString().Length == 36).WithMessage("O campo {PropertyName} tem que possuir 36 digitos.");

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
