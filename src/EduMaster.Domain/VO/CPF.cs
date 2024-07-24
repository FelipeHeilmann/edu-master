using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EduMaster.Domain.Exceptions;

namespace EduMaster.Domain.VO;
public record CPF
{
    private const int FACTOR_FIRST_DIGIT = 10;
    private const int FACTOR_SECOND_DIGIT = 11;
    public string Value { get; init; }
    public string Formatted => Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");

    public CPF(string cpf)
    {
        if (!Validate(cpf)) throw new InvalidCPF();
        Value = RemoveNonDigits(cpf);
    }

    private bool Validate(string rawCpf)
    {
        if (string.IsNullOrWhiteSpace(rawCpf)) return false;
        var cpf = RemoveNonDigits(rawCpf);
        if(!IsValidLength(cpf)) return false;
        if(AllDigitsEqual(cpf)) return false;
        var firstDigit = CalculateDigit(cpf, FACTOR_FIRST_DIGIT);
        var secondDigit = CalculateDigit(cpf, FACTOR_SECOND_DIGIT);
        return ExtractDigit(cpf) == $"{firstDigit}{secondDigit}";
    }

    private string RemoveNonDigits(string cpf)
    {
        return Regex.Replace(cpf, @"[^\d]", "");
    }

    private bool IsValidLength(string cpf)
    {
        return cpf.Length == 11;
    }

    private bool AllDigitsEqual(string cpf)
    {
        char firstDigit = cpf[0];
        return cpf.All(digit => digit == firstDigit);
    }

    private int CalculateDigit(string cpf, int factor)
    {
        var total = 0;
        foreach (char digit in cpf)
        {
            if (factor > 1) total += int.Parse(digit.ToString()) * factor--;
        }
        var remainder = total % 11;
        return (remainder < 2) ? 0 : 11 - remainder;
    }

    private string ExtractDigit(string cpf)
    {
        return cpf.Substring(9);
    }
}