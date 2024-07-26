using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Command.Login;

public record LoginCommand(string EmailOrRegistrationNumber, string Password) : ICommand<string>;

