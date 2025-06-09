using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Data.Abstractions;
using Notes.Application.Entities.Notes.Extensions;

namespace Notes.Application.Data;

public class NotesService(INotesRepository notesRepository, IServiceProvider serviceProvider) : INotesService
{
    private INodesService NodesService => serviceProvider.GetRequiredService<INodesService>();

    public async Task<List<NoteDto>> ListNotesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await NodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var notes = await notesRepository.ListNotesPaginatedAsync(n => !n.IsDeleted, pageIndex, pageSize, cancellationToken);

        // Exclude Notes whose Nodes have been deleted
        var noteDtos = notes
            .Select(n => (Note: n, Node: nodes.FirstOrDefault(d => d.Id == n.NodeId.ToString())))
            .Where(x => x.Node != null)
            .Select(x => x.Note.ToNoteDto(x.Node!))
            .ToList();

        // ToDo: Handle orphaned entities (Files, Notes, Reminders, but Nodes as well)

        return noteDtos;
    }

    public async Task<List<NoteDto>> ListFlaggedForDeletionNotesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await NodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var notes = await notesRepository.ListNotesPaginatedAsync(n => n.IsDeleted, pageIndex, pageSize, cancellationToken);

        // Exclude Notes whose Nodes have been deleted
        var noteDtos = notes
            .Select(n => (Note: n, Node: nodes.FirstOrDefault(d => d.Id == n.NodeId.ToString())))
            .Where(x => x.Node != null) // todo: changes might be needed due to complete deletion overhaul 
            .Select(x => x.Note.ToNoteDto(x.Node!))
            .ToList();

        return noteDtos;
    }

    public async Task<List<NoteBaseDto>> ListNotesByNodeIdPaginated(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var notes = await notesRepository.ListNotesPaginatedAsync(n => n.NodeId == nodeId, pageIndex, pageSize, cancellationToken);

        var notesDtos = notes
            .Select(n => n.ToNoteBaseDto())
            .ToList();

        return notesDtos;
    }

    public async Task<NoteDto> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        var noteDto = note.Adapt<NoteDto>();

        var node = await NodesService.GetNodeBaseByIdAsync(note.NodeId, cancellationToken);
        noteDto.Node = node;

        return noteDto;
    }

    public async Task<NoteBaseDto> GetNoteBaseByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        var noteBaseDto = note.Adapt<NoteBaseDto>();

        return noteBaseDto;
    }

    public async Task<long> CountAllNotesByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await notesRepository.CountAllNotesByNodeIdAsync(nodeId, cancellationToken);
    }

    public async Task DeleteNote(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        await NodesService.RemoveNote(note.NodeId, noteId, cancellationToken);

        await notesRepository.DeleteNote(noteId, cancellationToken);
    }

    public async Task DeleteNotes(NodeId nodeId, IEnumerable<NoteId> noteIds, CancellationToken cancellationToken)
    {
        var input = noteIds.ToList();

        await notesRepository.DeleteNotes(input, cancellationToken);
    }

    public async Task DeleteNotesByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        await notesRepository.DeleteNotesByNodeIds(nodeIds, cancellationToken);
    }

    public async Task<List<NoteBaseDto>> GetNotesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var notes = await notesRepository.GetNotesBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var noteBaseDtos = notes.Adapt<List<NoteBaseDto>>();
        return noteBaseDtos;
    }

    public async Task ReviveNote(NoteId noteId, CancellationToken cancellationToken)
    {
        await notesRepository.ReviveNoteAsync(noteId, cancellationToken);
    }

    public async Task<long> CountAllNotesAsync(CancellationToken cancellationToken)
    {
        return await notesRepository.CountAllNotesAsync(cancellationToken);
    }
}
