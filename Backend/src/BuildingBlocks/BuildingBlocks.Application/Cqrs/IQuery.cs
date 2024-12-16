using MediatR;

namespace BuildingBlocks.Application.Cqrs;

public interface IQuery : IQuery<Unit> { }

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull { }
