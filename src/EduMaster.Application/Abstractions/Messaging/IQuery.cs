using EduMaster.Domain.Shared;
using MediatR;

namespace EduMaster.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }