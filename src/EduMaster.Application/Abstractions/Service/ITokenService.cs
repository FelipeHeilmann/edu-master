namespace EduMaster.Application.Abstractions.Service;

public interface ITokenService
{
    public string GenerateAuthToken(Guid id, string email);        
}
