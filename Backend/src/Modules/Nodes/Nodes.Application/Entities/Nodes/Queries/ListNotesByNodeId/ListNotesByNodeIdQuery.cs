using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;

// ReSharper disable ClassNeverInstantiated.Global

namespace Nodes.Application.Entities.Nodes.Queries.ListNotesByNodeId;

public record ListNotesByNodeIdQuery(NodeId Id, PaginationRequest PaginationRequest) : IQuery<ListNotesByNodeIdResult>
{
    public ListNotesByNodeIdQuery(string id, PaginationRequest paginationRequest)
        : this(NodeId.Of(Guid.Parse(id)), paginationRequest) { }
}

public record ListNotesByNodeIdResult(PaginatedResult<NoteBaseDto> Notes);
