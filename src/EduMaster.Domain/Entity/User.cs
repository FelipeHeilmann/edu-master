using EduMaster.Domain.VO;

namespace EduMaster.Domain.Entity;
public class User
{  
    public Guid Id { get; private set; }
    public string? RegistrationNumber  { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Email => _email.Value;
    public string Password => _password.Value;
    public string PhoneFormatted => _phone.Formatted;
    public string Phone => _phone.Value;
    public string CPFFormatted => _cpf.Formatted;
    public string CPF => _cpf.Value;
    public string Status { get; private set; }
    public string Role { get; private set; } = string.Empty;
    public DateTime BirthDate { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    private Email _email;
    private Password _password;
    private Phone _phone;
    private CPF _cpf;

    protected User(Guid id, string name, Email email, Password password, Phone phone, CPF cpf, string role, string status, string? registrationNumber ,DateTime birthDate, DateTime enrollmentDate, DateTime createdAt)
    {
        Id = id;
        Name = name;
        RegistrationNumber = registrationNumber;
        Status = status;
        _email = email;
        _password = password;
        _phone = phone;
        _cpf = cpf;
        Role = role;
        BirthDate = birthDate;
        EnrollmentDate = enrollmentDate;
        CreatedAt = createdAt; 
    }

    public static User Create(string name, string email, string password, string phone, string cpf ,string role, DateTime birthDate, DateTime enrollmentDate)
    {
        var status = "active";
        var roles = new [] { "student", "professor", "admin" };
        if(!roles.Contains(role)) throw new Exception("Invalid role");
        return new User(Guid.NewGuid(), name, new Email(email), BcryptPassword.Create(password), new Phone(phone), new CPF(cpf), role, status, GenerateRegistrationNumber(role) ,birthDate, enrollmentDate, DateTime.UtcNow);
    }

    public bool PasswordMatches(string password)
    {
        return _password.PasswordMatches(password);
    }

    private static string? GenerateRegistrationNumber(string role)
    {
        if(role == "student")
        {
            return $"RM{new Random().Next(10000, 99999)}";
        }
        else if(role == "professor")
        {
            return $"P{new Random().Next(10000, 99999)}";
        }
        return null;
    }

}
