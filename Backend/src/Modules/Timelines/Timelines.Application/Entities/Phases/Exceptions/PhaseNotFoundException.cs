namespace Timelines.Application.Entities.Phases.Exceptions;

public class PhaseNotFoundException(string id) : NotFoundException("Node", id);
