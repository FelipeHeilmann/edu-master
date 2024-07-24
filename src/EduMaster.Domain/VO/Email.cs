
using System.Text.RegularExpressions;
using EduMaster.Domain.Exceptions;

namespace EduMaster.Domain.VO;
public record Email
{
    public string Value { get; init; }

    public Email(string email) 
    {
        if (string.IsNullOrWhiteSpace(email)) throw new InvalidEmail();

        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!regex.Match(email).Success) throw new InvalidEmail();

        Value = email;
    }
}
