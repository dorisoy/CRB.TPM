using System.Text.RegularExpressions;
using FluentValidation.Validators;
using FluentValidation;
using System.Collections.Generic;

namespace CRB.TPM.Validation.FluentValidation.Validators;

/// <summary>
/// Url验证
/// </summary>
public class UrlValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private const string Pattern = @"^(https?)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
    private static Regex _regex;
    public override string Name => "UrlValidator";

    public UrlValidator()
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

    protected override string GetDefaultMessageTemplate(string errorCode) => "URL地址无效";
}

