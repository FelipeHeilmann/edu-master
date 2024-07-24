using System.Text.RegularExpressions;
using EduMaster.Domain.Exceptions;

namespace EduMaster.Domain.VO;

public record Phone
{
    public string Value { get; }
    public string Formatted => Regex.Replace(Value, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");

    public Phone(string phone) 
    {
        if (string.IsNullOrEmpty(phone)) throw new InvalidPhone();

        string cleanedPhone = Regex.Replace(phone, @"[^\d]", "");

        var regex = new Regex(@"^\d{2}\d{4,5}\d{4}$");

        if (!regex.IsMatch(cleanedPhone)) throw new InvalidPhone();

        Value = cleanedPhone;
    }

}