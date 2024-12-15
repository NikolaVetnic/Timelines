using MediatR;

namespace BuildingBlocks.Cqrs;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IQuery<TResponse> 
    where TResponse : notnull { }
