using EduMaster.Domain.Shared;
using MediatR;

namespace EduMaster.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand{}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand {}

public interface IBaseCommand {}