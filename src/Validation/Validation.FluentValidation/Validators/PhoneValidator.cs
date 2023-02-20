using System.Text.RegularExpressions;
using FluentValidation.Validators;
using FluentValidation;
using System.Collections.Generic;


namespace CRB.TPM.Validation.FluentValidation.Validators;

/// <summary>
/// 手机号简单验证
/// </summary>
public class PhoneValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private const string Pattern = @"^1[345789]\d{9}$";
    private static Regex _regex;
    public override string Name => "PhoneValidator";

    public PhoneValidator()
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

    protected override string GetDefaultMessageTemplate(string errorCode) => "手机号无效";
}
