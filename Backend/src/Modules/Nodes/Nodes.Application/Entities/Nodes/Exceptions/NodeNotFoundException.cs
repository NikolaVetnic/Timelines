namespace Nodes.Application.Entities.Nodes.Exceptions;

public class NodeNotFoundException(string id) : NotFoundException("Node", id);
