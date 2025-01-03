using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Api.Converters;

public class NoteIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<NoteId, NoteId>().ConstructUsing(src => NoteId.Of(src.Value));
}
