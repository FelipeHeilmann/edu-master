using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Users.Create;
public record CreateUserCommand(string Name, 
                                string Email, 
                                string Password, 
                                string Phone, 
                                string CPF, 
                                DateTime BirthDate, 
                                DateTime EnrollmentDate,
                                string Role
                            ) : ICommand<Guid>;
                                               
