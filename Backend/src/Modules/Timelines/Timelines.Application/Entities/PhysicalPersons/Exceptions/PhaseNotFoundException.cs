namespace Timelines.Application.Entities.PhysicalPersons.Exceptions;

public class PhysicalPersonNotFoundException(string id) : NotFoundException("Phase", id);
