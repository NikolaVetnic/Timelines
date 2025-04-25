using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Domain.Notes.Note.Events;

public record NoteUpdatedEvent(NoteId NoteId) : IDomainEvent;
