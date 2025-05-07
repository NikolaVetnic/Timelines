using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace BuildingBlocks.Api.Converters;

public class PhaseIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<PhaseId, PhaseId>().ConstructUsing(src => PhaseId.Of(src.Value));
}
