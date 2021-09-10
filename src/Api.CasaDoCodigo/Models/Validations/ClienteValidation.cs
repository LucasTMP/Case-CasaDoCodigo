using Api.CasaDoCodigo.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {

        public ClienteValidation()
        {
            RuleFor(parameter => parameter.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Sobrenome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            //RuleFor(parameter => parameter.Documento)
            //    .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
            //    .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
            //    .Length(11, 14).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            When(parameter => parameter.Documento.Length == 11, () =>
             {
                 RuleFor(parameter => parameter.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                     .WithMessage("O campo Documento precisa ter 11 caracteres e foi fornecido {PropertyValue}.kkkkW");
                 RuleFor(parameter => CpfValidacao.Validar(parameter.Documento)).Equal(true)
                     .WithMessage("O documento CPF fornecido é inválido.");
             });

            When(parameter => parameter.Documento.Length == 14, () =>
             {
                 RuleFor(parameter => parameter.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                     .WithMessage("O campo Documento precisa ter 14 caracteres e foi fornecido {PropertyValue}.");
                 RuleFor(parameter => CnpjValidacao.Validar(parameter.Documento)).Equal(true)
                     .WithMessage("O documento CNPJ fornecido é inválido.");
             });

            //RuleFor(parameter => parameter.Documento.Length).Equal(11).Equal(14)
            //    .WithMessage("O campo Documento precisa ter 11 ou 14  caracteres.");
            //RuleFor(parameter => CnpjValidacao.Validar(parameter.Documento)).Equal(true)
            //    .WithMessage("O documento fornecido é inválido.");


            RuleFor(parameter => parameter.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("O {PropertyName} cadastrado precisa ser válido.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Endereco)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Complemento)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Cidade)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(1, 50).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.PaisId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Must(o => o.ToString().Length == 36).WithMessage("O campo {PropertyName} tem que possuir 36 digitos.");

            RuleFor(parameter => parameter.EstadoId)
                .Must(o => o.ToString().Length == 36).WithMessage("O campo {PropertyName} tem que possuir 36 digitos.");

            RuleFor(parameter => parameter.Telefone)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(10, 11).WithMessage("O campo {PropertyName} tem que possuir entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(parameter => parameter.Cep)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode estar em branco.")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo.")
                .Length(8, 8).WithMessage("O campo {PropertyName} tem que possuir {MaxLength} caracteres.");

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
