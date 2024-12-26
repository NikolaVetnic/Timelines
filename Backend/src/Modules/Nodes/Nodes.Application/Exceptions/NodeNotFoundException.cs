namespace Nodes.Application.Exceptions;

public class NodeNotFoundException(string id) : NotFoundException("Node", id);
