﻿using BuildingBlocks.Application.Pagination;
using Notes.Application.Entities.Notes.Dtos;

namespace Notes.Application.Entities.Notes.Queries.ListNotes;

public record ListNotesQuery(PaginationRequest PaginationRequest) : IQuery<ListNotesResult>;

public record ListNotesResult(PaginatedResult<NoteDto> Notes);