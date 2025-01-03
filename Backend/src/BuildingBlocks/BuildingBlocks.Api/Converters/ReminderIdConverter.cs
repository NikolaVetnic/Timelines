using BuildingBlocks.Domain.ValueObjects.Ids;

namespace BuildingBlocks.Api.Converters;

public class ReminderIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<ReminderId, ReminderId>().ConstructUsing(src => ReminderId.Of(src.Value));
}
