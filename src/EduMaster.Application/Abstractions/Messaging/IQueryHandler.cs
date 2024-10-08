using EduMaster.Domain.Shared;
using MediatR;

namespace EduMaster.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse> {} 

