using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Domain.Notes.Note.Events;

public record NoteUpdatedEvent(NoteId NoteId) : IDomainEvent;
