using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.Validations
{
    public class PaisValidation : AbstractValidator<Pais>
    {

        public PaisValidation()
        {
            RuleFor(parameter => parameter.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLenght} caracteres.");
        }

    }
}
