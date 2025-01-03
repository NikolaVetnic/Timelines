namespace Timelines.Application.Entities.Timelines.Exceptions;

public class TimelineNotFoundException(string id) : NotFoundException("Timeline", id);
