using MediatR;

namespace BuildingBlocks.Cqrs;

public interface ICommand : ICommand<Unit> { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }