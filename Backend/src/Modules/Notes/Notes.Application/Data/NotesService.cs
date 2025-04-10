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
    public async Task<List<NoteDto>> ListNotesPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodesService = serviceProvider.GetRequiredService<INodesService>();
        var nodes = await nodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var notes = await notesRepository.ListNotesPaginatedAsync(pageIndex, pageSize, cancellationToken);

        var noteDtos = notes.Select(n =>
            n.ToNoteDto(
                
                nodes.First(d => d.Id == n.NodeId.ToString())
                )).ToList();

        return noteDtos;
    }

    public async Task<NoteDto> GetNoteByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        var noteDto = note.Adapt<NoteDto>();

        var node = await serviceProvider.GetRequiredService<INodesService>().GetNodeBaseByIdAsync(note.NodeId, cancellationToken);
        noteDto.Node = node;

        return noteDto;
    }

    public async Task<NoteBaseDto> GetNoteBaseByIdAsync(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);
        var noteBaseDto = note.Adapt<NoteBaseDto>();

        return noteBaseDto;
    }

    public async Task DeleteNote(NoteId noteId, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetNoteByIdAsync(noteId, cancellationToken);

        var nodeServices = serviceProvider.GetRequiredService<INodesService>();
        await nodeServices.RemoveNote(note.NodeId, noteId, cancellationToken);

        await notesRepository.DeleteNote(noteId, cancellationToken);
    }

    public async Task DeleteNotes(NodeId nodeId, IEnumerable<NoteId> noteIds, CancellationToken cancellationToken)
    {
        var input = noteIds.ToList();

        var nodeServices = serviceProvider.GetRequiredService<INodesService>();
        await nodeServices.RemoveNotes(nodeId, input, cancellationToken);

        await notesRepository.DeleteNotes(input, cancellationToken);
    }

    public async Task<List<NoteBaseDto>> GetNotesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var notes = await notesRepository.GetNotesBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var noteBaseDtos = notes.Adapt<List<NoteBaseDto>>();
        return noteBaseDtos;
    }

    public async Task<long> CountNotesAsync(CancellationToken cancellationToken)
    {
        return await notesRepository.NoteCountAsync(cancellationToken);
    }
}
