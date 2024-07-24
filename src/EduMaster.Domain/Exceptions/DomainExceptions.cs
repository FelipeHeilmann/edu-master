
namespace EduMaster.Domain.Exceptions;

public class InvalidEmail : Exception 
{
    public InvalidEmail() : base("Invalid email") {}
}


public class InvalidPhone : Exception
{
    public InvalidPhone() : base("Invalid phone") {}
}

public class InvalidPasswordLenght : Exception 
{
    public InvalidPasswordLenght() : base("Invalid password length") {}
}

public class InvalidCPF : Exception
{
    public InvalidCPF() : base("Invalid cpf") {}
}