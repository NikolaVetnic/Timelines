// ReSharper disable ClassNeverInstantiated.Global

using Notes.Application.Entities.Notes.Dtos;

namespace Notes.Application.Entities.Notes.Queries.GetNoteById;

public record GetNoteByIdQuery(NoteId Id) : IQuery<GetNoteByIdResult>
{
    public GetNoteByIdQuery(string Id) : this(NoteId.Of(Guid.Parse(Id))) { }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetNoteByIdResult(NoteDto NoteDto);

public class GetNoteByIdQueryValidator : AbstractValidator<GetNoteByIdQuery>
{
    public GetNoteByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
