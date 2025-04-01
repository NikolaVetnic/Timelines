using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Data.Abstractions;

namespace Notes.Application.Data;

public class NotesService(INotesRepository notesRepository, IServiceProvider serviceProvider) : INotesService
{
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
        await notesRepository.DeleteNote(noteId, cancellationToken);
    }

    public async Task<List<NoteBaseDto>> GetNotesBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var notes = await notesRepository.GetNotesBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var noteBaseDtos = notes.Adapt<List<NoteBaseDto>>();
        return noteBaseDtos;
    }
}
