using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Query.GetUser;

public record Output(Guid Id, 
                    string Name, 
                    string Email, 
                    string Phone, 
                    string CPF, 
                    string Status, 
                    string Role, 
                    string BirthDate, 
                    string EnrollmentDate);
public record GetUserQuery(Guid Id) : IQuery<Output>;