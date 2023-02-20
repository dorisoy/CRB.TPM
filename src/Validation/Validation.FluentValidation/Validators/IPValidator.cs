using System.Text.RegularExpressions;
using FluentValidation.Validators;
using FluentValidation;
using System.Collections.Generic;


namespace CRB.TPM.Validation.FluentValidation.Validators;

/// <summary>
/// IP验证
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TProperty"></typeparam>
public class IPValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private const string Pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
    private static Regex _regex;
    public override string Name => "IPValidator";

    public IPValidator()
    {
        _regex = new Regex(Pattern);
    }

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (string.IsNullOrEmpty(value.ToString()))
        {
            return false;
        }

        return _regex.IsMatch(value.ToString());
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "IP地址无效";
}
