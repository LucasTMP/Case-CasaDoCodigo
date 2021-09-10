using Api.CasaDoCodigo.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
internal class NonEmptyGuidAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if ((value is Guid) && Guid.Empty == (Guid)value)
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
        return null;
    }
}


[AttributeUsage(AttributeTargets.Property)]
internal class NonEmptyDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if ((value is DateTime) && DateTime.MinValue == (DateTime)value)
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName)); ;
        }
        return null;
    }
}

[AttributeUsage(AttributeTargets.Property)]
internal class IsValidUri : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        if (!(Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute)) || value.ToString() == "")
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
        return null;
    }
}


[AttributeUsage(AttributeTargets.Property)]
internal class IsValidDocumento : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || value.ToString() == "")
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        if (CpfValidacao.TamanhoCpf == value.ToString().Length)
        {
            if (!CpfValidacao.Validar(value.ToString()))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else
            {
                return null;
            }
        }

        if (CnpjValidacao.TamanhoCnpj == value.ToString().Length)
        {
            if (!CnpjValidacao.Validar(value.ToString()))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else
            {
                return null;
            }
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

    }
}