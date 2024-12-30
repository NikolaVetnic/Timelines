namespace Notes.Application.Entities.Notes.Exceptions;

public class NoteNotFoundException(string id) : NotFoundException("Note", id);
