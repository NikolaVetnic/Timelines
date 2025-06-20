﻿using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;

namespace BuildingBlocks.Domain.Notes.Note.Events
{
    public record NoteCreatedEvent(NoteId NoteId) : IDomainEvent;
}
